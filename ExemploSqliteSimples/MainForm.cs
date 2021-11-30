/*
 * Criado por SharpDevelop.
 * Usuário: PCDG
 * Data: 19/11/2021
 * Hora: 23:42
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO.Ports;
using System.Data;

//using csharp_Sqlite.Models;



namespace ExemploSqliteSimples
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private SQLiteConnection sql_con;
		private SQLiteCommand sql_cmd;
		private SQLiteDataAdapter DB;
		private DataSet DS = new DataSet();
		private DataTable DT = new DataTable();
		private SQLiteDataReader DR;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		private void LoadData()
		{
			SetConnection(); 
			sql_con.Open();

			sql_cmd = sql_con.CreateCommand();
			string CommandText = "select id, senha,status from  clientes";
			DB = new SQLiteDataAdapter(CommandText,sql_con);
            DS.Reset();
			DB.Fill(DS);
			DT= DS.Tables[0];			
			Grid.DataSource = DT;
			sql_con.Close();			
		}
		
        private void SetConnection()
        {
			sql_con = new SQLiteConnection("Data Source=clientes.db;Version=3;New=False;Compress=True;");
			
        }
		private void ExecuteQuery(string txtQuery)
		{
			SetConnection();
			sql_con.Open();
          
			sql_cmd = sql_con.CreateCommand();
			sql_cmd.CommandText=txtQuery;
		
			sql_cmd.ExecuteNonQuery();
			sql_con.Close();
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			LoadData();
		}
		void BtAdicionarClick(object sender, EventArgs e)
		{
			Add();
	        LoadData();
		}
		
		private void Add()
		{
		    string txtSQLQuery = "insert into  clientes (id,senha,status) values (" + txtID.Text + "," + txtSenha.Text + "," + txtStatus.Text + ")"; 
		    ExecuteQuery(txtSQLQuery);
			
		}
		void BtExcluirClick(object sender, EventArgs e)
		{
			Delete();
		    LoadData();
		}
		private void Delete()
		{
			string txtSQLQuery = "delete from  clientes where id ="+txtID.Text;
			ExecuteQuery(txtSQLQuery);
			txtID.Text = string.Empty;
			txtSenha.Text = string.Empty;
			txtStatus.Text = string.Empty;
			LoadData();
		}
		void BtModificarClick(object sender, EventArgs e)
		{
			Edit();
			LoadData();
		}	
       private void Edit()
	   {
		string txtSQLQuery = "update  clientes set  senha ='"+txtSenha.Text+"',status ='"+txtStatus.Text+"' where id ="+txtID.Text;
		ExecuteQuery(txtSQLQuery);   
	   }	
       
		void BtPesquisasr(object sender, EventArgs e){       	
       	LoadUmCliente(txtID.Text);	       	
		}	
       
		private void LoadUmCliente(String id){
			SetConnection(); 
			sql_con.Open();

			sql_cmd = sql_con.CreateCommand();
			string CommandText = "select id, senha,status from  clientes where id=" + id;
			DB = new SQLiteDataAdapter(CommandText,sql_con);
            DS.Reset();
			DB.Fill(DS);
			DT= DS.Tables[0];			
			Grid.DataSource = DT;
			sql_con.Close();
			
		}
		void BtSairClick(object sender, EventArgs e){
	       Application.Exit();
		}
		
		//Receber do PIC
		void BtDownloadClick(object sender, EventArgs e){
			while(serialPort1.ReadLine() != "!"){
		    String id;
			String senha;
			String status;			
			id = serialPort1.ReadLine();
			senha = serialPort1.ReadLine();
			status = serialPort1.ReadLine();
			
			string txtSQLQuery = "insert into  clientes (id,senha,status) values (" + id + "," + senha + "," + status + ")"; 
		    ExecuteQuery(txtSQLQuery);
			}
		}
		
		// Mandar para o PIC
		void BtUploadClick(object sender, EventArgs e){
			
			SetConnection(); 
			sql_con.Open();
			
			string CommandText = "select * from  clientes";	
			
			sql_cmd = new SQLiteCommand(CommandText,sql_con);
			DR = sql_cmd.ExecuteReader();         		
			
			char[] dados = new Char[7];
			
			String id;
			String senha;
			String status;
			
			while(DR.Read()){
				if (serialPort1.IsOpen){
				
				serialPort1.Write("\r");
				
				id = Convert.ToString(DR["id"]);
				senha = Convert.ToString(DR["senha"]);
				status = Convert.ToString(DR["status"]);
				
				dados[0] = id.ToCharArray()[0];
				dados[1] = id.ToCharArray()[1];
				dados[2] = senha.ToCharArray()[0];
				dados[3] = senha.ToCharArray()[1];
				dados[4] = senha.ToCharArray()[2];
				dados[5] = senha.ToCharArray()[3];
				dados[6] = status.ToCharArray()[0];
				
				for(int i = 0; i < 7; i++){
					serialPort1.Write(dados,i,1);
					Thread.Sleep(100);
				}
				
				serialPort1.Write("\n");
				}
			}			
			
			sql_con.Close();
			
		}
		
		private void atualizaListaCOMs()
        {
            int i;
            bool quantDiferente;    //flag para sinalizar que a quantidade de portas mudou
 
            i = 0;
            quantDiferente = false;
 
            //se a quantidade de portas mudou
            if (comboBox1.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (comboBox1.Items[i++].Equals(s) == false)
                    {
                        quantDiferente = true;
                    }
                }
            }
            else
            {
                quantDiferente = true;
            }
 
            //Se não foi detectado diferença
            if (quantDiferente == false)
            {
                return;                     //retorna
            }
 
            //limpa comboBox
            comboBox1.Items.Clear();
 
            //adiciona todas as COM diponíveis na lista
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
            //seleciona a primeira posição da lista
            comboBox1.SelectedIndex = 0;
        }
		
		
		
		void Timer1Tick(object sender, EventArgs e)
		{
			 atualizaListaCOMs();
		}
		
		
		
		void BtnConectarClick(object sender, EventArgs e)
		{
			 if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.PortName = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                    serialPort1.Open();
 
                }
                catch
                {
                    return;
 
                }
                if (serialPort1.IsOpen)
                {
                    btnConectar.Text = "Desconectar";
					btnConectar.BackColor = System.Drawing.Color.Red;                    
                    comboBox1.Enabled = false;
 
                }
            }
            else{
 
                try
                {
                    serialPort1.Close();
                    comboBox1.Enabled = true;
                    btnConectar.BackColor = System.Drawing.Color.LightGreen; 
                    btnConectar.Text = "Conectar";
                }
                catch
                {
                    return;
                }
 
            }
        			
		}
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			
		}
		
		void GridNavigate(object sender, NavigateEventArgs ne){
			
		}
		
		void BtnShowAllClick(object sender, EventArgs e){
			LoadData();			
		}
	}
}
