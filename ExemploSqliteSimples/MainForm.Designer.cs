/*
 * Criado por SharpDevelop.
 * Usuário: PCDG
 * Data: 19/11/2021
 * Hora: 23:42
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
namespace ExemploSqliteSimples
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.DataGrid Grid;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btDownload;
		private System.Windows.Forms.Button btUpload;
		private System.Windows.Forms.Button btSair;
		private System.Windows.Forms.Button btExcluir;
		private System.Windows.Forms.Button btAdicionar;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtStatus;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtSenha;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtID;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.Button btModificar;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.Grid = new System.Windows.Forms.DataGrid();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnShowAll = new System.Windows.Forms.Button();
			this.btnPesquisar = new System.Windows.Forms.Button();
			this.btModificar = new System.Windows.Forms.Button();
			this.btSair = new System.Windows.Forms.Button();
			this.btExcluir = new System.Windows.Forms.Button();
			this.btAdicionar = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.txtStatus = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtSenha = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtID = new System.Windows.Forms.TextBox();
			this.btDownload = new System.Windows.Forms.Button();
			this.btUpload = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.btnConectar = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
			((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Grid
			// 
			this.Grid.AlternatingBackColor = System.Drawing.Color.WhiteSmoke;
			this.Grid.BackColor = System.Drawing.Color.Gainsboro;
			this.Grid.BackgroundColor = System.Drawing.Color.DarkGray;
			this.Grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Grid.CaptionBackColor = System.Drawing.Color.DarkKhaki;
			this.Grid.CaptionFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.Grid.CaptionForeColor = System.Drawing.Color.Black;
			this.Grid.DataMember = "";
			this.Grid.FlatMode = true;
			this.Grid.Font = new System.Drawing.Font("Times New Roman", 9F);
			this.Grid.ForeColor = System.Drawing.Color.Black;
			this.Grid.GridLineColor = System.Drawing.Color.Silver;
			this.Grid.HeaderBackColor = System.Drawing.Color.Black;
			this.Grid.HeaderFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.Grid.HeaderForeColor = System.Drawing.Color.White;
			this.Grid.LinkColor = System.Drawing.Color.DarkSlateBlue;
			this.Grid.Location = new System.Drawing.Point(5, 153);
			this.Grid.Name = "Grid";
			this.Grid.ParentRowsBackColor = System.Drawing.Color.LightGray;
			this.Grid.ParentRowsForeColor = System.Drawing.Color.Black;
			this.Grid.PreferredColumnWidth = 120;
			this.Grid.PreferredRowHeight = 25;
			this.Grid.ReadOnly = true;
			this.Grid.SelectionBackColor = System.Drawing.Color.Firebrick;
			this.Grid.SelectionForeColor = System.Drawing.Color.White;
			this.Grid.Size = new System.Drawing.Size(483, 127);
			this.Grid.TabIndex = 1;
			this.Grid.Navigate += new System.Windows.Forms.NavigateEventHandler(this.GridNavigate);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnShowAll);
			this.groupBox1.Controls.Add(this.btnPesquisar);
			this.groupBox1.Controls.Add(this.btModificar);
			this.groupBox1.Controls.Add(this.btSair);
			this.groupBox1.Controls.Add(this.btExcluir);
			this.groupBox1.Controls.Add(this.btAdicionar);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtStatus);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtSenha);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtID);
			this.groupBox1.Location = new System.Drawing.Point(5, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(483, 100);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			// 
			// btnShowAll
			// 
			this.btnShowAll.Location = new System.Drawing.Point(230, 68);
			this.btnShowAll.Name = "btnShowAll";
			this.btnShowAll.Size = new System.Drawing.Size(79, 23);
			this.btnShowAll.TabIndex = 14;
			this.btnShowAll.Text = "Mostar Tudo";
			this.btnShowAll.UseVisualStyleBackColor = true;
			this.btnShowAll.Click += new System.EventHandler(this.BtnShowAllClick);
			// 
			// btnPesquisar
			// 
			this.btnPesquisar.Location = new System.Drawing.Point(149, 69);
			this.btnPesquisar.Name = "btnPesquisar";
			this.btnPesquisar.Size = new System.Drawing.Size(77, 23);
			this.btnPesquisar.TabIndex = 13;
			this.btnPesquisar.Text = "Pesquisar";
			this.btnPesquisar.UseVisualStyleBackColor = true;
			this.btnPesquisar.Click += new System.EventHandler(this.BtPesquisasr);
			// 
			// btModificar
			// 
			this.btModificar.Location = new System.Drawing.Point(313, 19);
			this.btModificar.Name = "btModificar";
			this.btModificar.Size = new System.Drawing.Size(79, 72);
			this.btModificar.TabIndex = 12;
			this.btModificar.Text = "Modificar";
			this.btModificar.UseVisualStyleBackColor = true;
			this.btModificar.Click += new System.EventHandler(this.BtModificarClick);
			// 
			// btSair
			// 
			this.btSair.Location = new System.Drawing.Point(398, 19);
			this.btSair.Name = "btSair";
			this.btSair.Size = new System.Drawing.Size(79, 74);
			this.btSair.TabIndex = 9;
			this.btSair.Text = "&Sair";
			this.btSair.UseVisualStyleBackColor = true;
			this.btSair.Click += new System.EventHandler(this.BtSairClick);
			// 
			// btExcluir
			// 
			this.btExcluir.BackColor = System.Drawing.Color.Red;
			this.btExcluir.Location = new System.Drawing.Point(230, 19);
			this.btExcluir.Name = "btExcluir";
			this.btExcluir.Size = new System.Drawing.Size(79, 46);
			this.btExcluir.TabIndex = 7;
			this.btExcluir.Text = "Excluir";
			this.btExcluir.UseVisualStyleBackColor = false;
			this.btExcluir.Click += new System.EventHandler(this.BtExcluirClick);
			// 
			// btAdicionar
			// 
			this.btAdicionar.BackColor = System.Drawing.Color.LawnGreen;
			this.btAdicionar.Location = new System.Drawing.Point(149, 19);
			this.btAdicionar.Name = "btAdicionar";
			this.btAdicionar.Size = new System.Drawing.Size(77, 46);
			this.btAdicionar.TabIndex = 6;
			this.btAdicionar.Text = "Adicionar";
			this.btAdicionar.UseVisualStyleBackColor = false;
			this.btAdicionar.Click += new System.EventHandler(this.BtAdicionarClick);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(7, 74);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46, 19);
			this.label3.TabIndex = 5;
			this.label3.Text = "Status:";
			// 
			// txtStatus
			// 
			this.txtStatus.Location = new System.Drawing.Point(59, 71);
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.Size = new System.Drawing.Size(84, 20);
			this.txtStatus.TabIndex = 4;
			this.txtStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(46, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "Senha:";
			// 
			// txtSenha
			// 
			this.txtSenha.Location = new System.Drawing.Point(59, 45);
			this.txtSenha.Name = "txtSenha";
			this.txtSenha.Size = new System.Drawing.Size(84, 20);
			this.txtSenha.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(27, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(26, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "ID:";
			// 
			// txtID
			// 
			this.txtID.Location = new System.Drawing.Point(59, 19);
			this.txtID.Name = "txtID";
			this.txtID.Size = new System.Drawing.Size(84, 20);
			this.txtID.TabIndex = 0;
			this.txtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// btDownload
			// 
			this.btDownload.BackColor = System.Drawing.Color.Azure;
			this.btDownload.Location = new System.Drawing.Point(86, 119);
			this.btDownload.Name = "btDownload";
			this.btDownload.Size = new System.Drawing.Size(90, 23);
			this.btDownload.TabIndex = 11;
			this.btDownload.Text = "Download<-PIC";
			this.btDownload.UseVisualStyleBackColor = false;
			this.btDownload.Click += new System.EventHandler(this.BtDownloadClick);
			// 
			// btUpload
			// 
			this.btUpload.BackColor = System.Drawing.Color.Azure;
			this.btUpload.Location = new System.Drawing.Point(5, 119);
			this.btUpload.Name = "btUpload";
			this.btUpload.Size = new System.Drawing.Size(75, 23);
			this.btUpload.TabIndex = 10;
			this.btUpload.Text = "Upload->PIC";
			this.btUpload.UseVisualStyleBackColor = false;
			this.btUpload.Click += new System.EventHandler(this.BtUploadClick);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 283);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(499, 22);
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.AutoSize = false;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(186, 121);
			this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(128, 21);
			this.comboBox1.TabIndex = 12;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1SelectedIndexChanged);
			// 
			// btnConectar
			// 
			this.btnConectar.BackColor = System.Drawing.Color.LightGreen;
			this.btnConectar.Location = new System.Drawing.Point(327, 112);
			this.btnConectar.Margin = new System.Windows.Forms.Padding(2);
			this.btnConectar.Name = "btnConectar";
			this.btnConectar.Size = new System.Drawing.Size(127, 36);
			this.btnConectar.TabIndex = 13;
			this.btnConectar.Text = "Conectar";
			this.btnConectar.UseVisualStyleBackColor = false;
			this.btnConectar.Click += new System.EventHandler(this.BtnConectarClick);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(499, 305);
			this.Controls.Add(this.btnConectar);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.btDownload);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btUpload);
			this.Controls.Add(this.Grid);
			this.Name = "MainForm";
			this.Text = "Exemplo Sqlite Simples";
			this.Load += new System.EventHandler(this.MainFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button btnShowAll;
		private System.Windows.Forms.Button btnPesquisar;
		private System.IO.Ports.SerialPort serialPort1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button btnConectar;
		private System.Windows.Forms.ComboBox comboBox1;
	}
}
