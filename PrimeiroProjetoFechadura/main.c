#include <main.h>

#include <2404.C>

#include <kbd_ext_board_int.c>

#include <mod_lcd.c>

#use rs232(baud = 9600, parity = N, xmit = PIN_C6, rcv = PIN_C7, bits = 8, STREAM = Wireless)

void imprimeCliente();
void recebeClienteBanco();
void cadastraCliente();
void deletarCliente();
void menuPrincipalADM();
void menuPrincipalUser();
void apagarMemoria();
void manutencao();
void atualizarBancoSQL();
void atualizaBancoPIC();

//parte de recebimento via serial
BOOLEAN recebendo_dados = FALSE;
BOOLEAN dado_disponivel = FALSE;
char dado;

#int_RDA
void RDA_isr(void) {
   recebendo_dados = TRUE;
   dado = getc();   
   dado_disponivel = TRUE;
}
//parte de recebimento via serial

typedef struct {
  char id[2];
  char senha[4];
  char status;
}
Cliente;

Cliente pessoa;
Cliente Adminstrador;
int flagAdm = 0;
int flagMenuAdm = 0;
char tecla;
int ja = 0;
int lastMemoryPosition = 7;
//teoricamente deveria funcionar, n?o est? n sei pq
#INT_EXT
void EXT_isr(void) {
  flagAdm = 0;
}

void main() {
  init_ext_eeprom();
  enable_interrupts(INT_EXT);
  enable_interrupts(GLOBAL);
  lcd_ini();
  delay_ms(50);
  while (TRUE) {
    char aux1;
    int f1 = 0;
    int y = 0;
    //apagarMemoria();  
    if (recebendo_dados) {    
    atualizarBancoSQL();      
    }
    lastMemoryPosition = 7;
    
    if (flagAdm == 0) {
      cadastraCliente();
    }
    if (flagMenuAdm == 0) {
      printf(lcd_escreve, "\fADM-1 | USER-2\n");
      // fica esperando o usuario digitar algo
      tecla = tc_tecla(1500);
      while (y == 0) {
        if (tecla == '@') {
          tecla = tc_tecla(1500);
        } else {
          y = 1;
        }
      }
      // acaba aqui
      printf(lcd_escreve, "%c", tecla);
      while (f1 != 1) {
        aux1 = tc_tecla(1500);
        if (aux1 == '#') {
          f1 = 1;
        }
      }
      switch (tecla) {
      case '1':
        menuPrincipalADM();
        break;
      case '2':
        menuPrincipalUser();
        break;
      }
    } else {
      manutencao();
    }
  }
}
void apagarMemoria() {  
  while (ja < 512) {
    write_ext_eeprom(ja, 0xff); // porque ta dando loop infinito de acordo com o programa 
    ja++;
  }
}

void menuPrincipalUser() {
  int aux = 0, k = 0, f = 0, y = 0;
  char aux1;
  char tcl;
  int local;
  int positionFindId = 0;
  Cliente c1;

  printf(lcd_escreve, "\fID:\n");
  while (aux < 2) {
    // fica esperando o usuario digitar algo
    tcl = tc_tecla(1500);
    while (y == 0) {
      if (tcl == '@') {
        tcl = tc_tecla(1500);
      } else {
        y = 1;
      }
    }
    // acaba aqui   
    printf(lcd_escreve, "%c", tcl);
    c1.id[aux] = tcl;
    aux++;
  }
  while (f != 1) {
    aux1 = tc_tecla(1500);
    if (aux1 == '#') {
      f = 1;
    }
  }
  f = 0;
  aux = 0;
  y = 0;
  printf(lcd_escreve, "\fDIGITE A SENHA: \n");
  while (aux < 4) {
    // fica esperando o usuario digitar algo
    tcl = tc_tecla(1500);
    while (y == 0) {
      if (tcl == '@') {
        tcl = tc_tecla(1500);
      } else {
        y = 1;
      }
    }
    // acaba aqui   
    printf(lcd_escreve, "%c", tcl);
    c1.senha[aux] = tcl;
    aux++;
  }
  while (f != 1) {
    aux1 = tc_tecla(1500);
    if (aux1 == '#') {
      f = 1;
    }
  }
  f = 0;
  while (k != 1) {
    local = positionFindId;
    if (read_ext_eeprom(local) == c1.id[0]) {
      if (read_ext_eeprom(local + 1) == c1.id[1]) {
        if (read_ext_eeprom(local + 2) == c1.senha[0]) {
          if (read_ext_eeprom(local + 3) == c1.senha[1]) {
            if (read_ext_eeprom(local + 4) == c1.senha[2]) {
              if (read_ext_eeprom(local + 5) == c1.senha[3]) {
                if (read_ext_eeprom(local + 6) == '1') {
                  printf(lcd_escreve, "\fBEM VINDO\n");
                  delay_ms(1500);
                  k = 1; //aqui eu acendo o LED
                } else {
                  printf(lcd_escreve, "\fINVALIDO\n");
                  delay_ms(1500);
                  k = 1;
                }
              } else {
                printf(lcd_escreve, "\fACESSO NEGADO\n");
                delay_ms(1500);
                k = 1;
              }
            } else {
              printf(lcd_escreve, "\fACESSO NEGADO\n");
              delay_ms(1500);
              k = 1;
            }
          } else {
            printf(lcd_escreve, "\fACESSO NEGADO\n");
            delay_ms(1500);
            k = 1;
          }
        } else {
          printf(lcd_escreve, "\fACESSO NEGADO\n");
          delay_ms(1500);
          k = 1;
        }
      } else {
        positionFindId += 7;
        if (positionFindId > 73) {
          printf(lcd_escreve, "\fID NOT FOUND\n");
          delay_ms(1500);
          k = 1;
        }
      }
    } else {
      positionFindId += 7;
      if (positionFindId > 73) {
        printf(lcd_escreve, "\fID NOT FOUND: \n");
        delay_ms(1500);
        k = 1;
      }
    }
  }
}
void manutencao() {
  int f = 0;
  int y = 0;
  char aux1;
  printf(lcd_escreve, "\fDIGITE OPCAO: ");
  // fica esperando o usuario digitar algo
  tecla = tc_tecla(1500);
  while (y == 0) {
    if (tecla == '@') {
      tecla = tc_tecla(1500);
    } else {
      y = 1;
    }
  }
  // acaba aqui  
  printf(lcd_escreve, "%c", tecla);
  while (f != 1) {
    aux1 = tc_tecla(1500);
    if (aux1 == '#') {
      f = 1;
    }
  }
  switch (tecla) {
  case '1':
    imprimeCliente();
    break;
  case '2':
    cadastraCliente();
    break;
  case '3':
    deletarCliente(); //como deletar o cliente da memoria
    break;
  case '4':
    atualizarBancoSQL();    
    break;
  case '5':
    flagMenuAdm = 0;
    break;
  }
}
void menuPrincipalADM() {
  char aux1;
  int aux = 0;
  int y = 0;
  int f = 0;
  char tcl;
  Cliente c1;
  // fazer uma rotina que valida o adm e ai vai pra esse menu aqui, tudo por causa da porra da recursividade que ele n aceita
  printf(lcd_escreve, "\fID ADM:\n");
  while (aux < 2) {
    // fica esperando o usuario digitar algo
    tcl = tc_tecla(1500);
    while (y == 0) {
      if (tcl == '@') {
        tcl = tc_tecla(1500);
      } else {
        y = 1;
      }
    }
    // acaba aqui   
    printf(lcd_escreve, "%c", tcl);
    c1.id[aux] = tcl;
    aux++;
  }
  while (f != 1) {
    aux1 = tc_tecla(1500);
    if (aux1 == '#') {
      f = 1;
    }
  }
  f = 0;
  aux1 = ' ';
  aux = 0;
  y = 0;
  printf(lcd_escreve, "\fDIGITE A SENHA: \n");
  while (aux < 4) {
    // fica esperando o usuario digitar algo
    tcl = tc_tecla(1500);
    while (y == 0) {
      if (tcl == '@') {
        tcl = tc_tecla(1500);
      } else {
        y = 1;
      }
    }
    // acaba aqui   
    printf(lcd_escreve, "%c", tcl);
    c1.senha[aux] = tcl;
    aux++;
  }
  while (f != 1) {
    aux1 = tc_tecla(1500);
    if (aux1 == '#') {
      f = 1;
    }
  }
  f = 0;
  while (f != 1) {
    aux1 = tc_tecla(1500);
    if (aux1 == '#') {
      f = 1;
    }
  }
  f = 0;
  if (read_ext_eeprom(0) == c1.id[0]) {
    if (read_ext_eeprom(1) == c1.id[1]) {
      if (read_ext_eeprom(2) == c1.senha[0]) {
        if (read_ext_eeprom(3) == c1.senha[1]) {
          if (read_ext_eeprom(4) == c1.senha[2]) {
            if (read_ext_eeprom(5) == c1.senha[3]) {
              if (read_ext_eeprom(6) == '1') {
                // flag para voltar pro menu de manute??o
                flagMenuAdm = 1;
                manutencao();
              } else {
                printf(lcd_escreve, "\fINVALIDO\n");
                delay_ms(1500);
              }
            } else {
              printf(lcd_escreve, "\fACESSO NEGADO\n");
              delay_ms(1500);
            }
          } else {
            printf(lcd_escreve, "\fACESSO NEGADO\n");
            delay_ms(1500);
          }
        } else {
          printf(lcd_escreve, "\fACESSO NEGADO\n");
          delay_ms(1500);
        }
      } else {
        printf(lcd_escreve, "\fACESSO NEGADO\n");
        delay_ms(1500);
      }
    } else {
      printf(lcd_escreve, "\fID NOT FOUND\n");
      delay_ms(1500);
    }
  } else {
    printf(lcd_escreve, "\fID NOT FOUND: \n");
    delay_ms(1500);
  }
}
void deletarCliente() {
  int f = 0, y = 0;
  int k = 0;
  char tcl;
  int aux = 0;
  Cliente teste;
  int local;
  int positionFindId = 0;
  printf(lcd_escreve, "\fID A DELETAR: \n");
  while (aux < 2) {
    // fica esperando o usuario digitar algo
    tcl = tc_tecla(1500);
    while (y == 0) {
      if (tcl == '@') {
        tcl = tc_tecla(1500);
      } else {
        y = 1;
      }
    }
    // acaba aqui   
    printf(lcd_escreve, "%c", tcl);
    teste.id[aux] = tcl;
    aux++;
  }
  while (f != 1) {
    tecla = tc_tecla(1500);
    if (tecla == '#') {
      f = 1;
    }
  }
  y = 0;
  f = 0;
  while (k != 1) {
    local = positionFindId;
    if (read_ext_eeprom(local) == teste.id[0]) {
      if (read_ext_eeprom(local + 1) == teste.id[1]) {
        write_ext_eeprom(local, 0xff);
        local++;
        write_ext_eeprom(local, 0xff);
        local++;
        write_ext_eeprom(local, 0xff);
        local++;
        write_ext_eeprom(local, 0xff);
        local++;
        write_ext_eeprom(local, 0xff);
        local++;
        write_ext_eeprom(local, 0xff);
        local++;
        write_ext_eeprom(local, 0xff);
        local++;
        printf(lcd_escreve, "\fID APAGADO");
        while (f != 1) {
          tecla = tc_tecla(1500);
          if (tecla == '#') {
            f = 1;
          }
        }
        k = 1;
      } else {
        positionFindId += 7;
        if (positionFindId > 73) {
          printf(lcd_escreve, "\fID NAO ENCONTRADO: \n");
          delay_ms(1500);
          k = 1;
        }
      }
    } else {
      positionFindId += 7;
      if (positionFindId > 73) {
        printf(lcd_escreve, "\fID NAO ENCONTRADO: \n");
        delay_ms(1500);
        k = 1;
      }
    }
  }
}
void imprimeCliente() { // Ver como apagar se errar
  int f = 0;
  int k = 0, y = 0;
  char tcl;
  int aux = 0;
  int local;
  Cliente teste;
  int positionFindId = 0;
  printf(lcd_escreve, "\fID DA BUSCA: \n");
  while (aux < 2) {
    // fica esperando o usuario digitar algo
    tcl = tc_tecla(1500);
    while (y == 0) {
      if (tcl == '@') {
        tcl = tc_tecla(1500);
      } else {
        y = 1;
      }
    }
    // acaba aqui   
    printf(lcd_escreve, "%c", tcl);
    teste.id[aux] = tcl;
    aux++;
  }
  while (f != 1) {
    tecla = tc_tecla(1500);
    if (tecla == '#') {
      f = 1;
    }
  }
  y = 0;
  f = 0;
  while (k != 1) {
    local = positionFindId;
    if (read_ext_eeprom(local) == teste.id[0]) {
      if (read_ext_eeprom(local + 1) == teste.id[1]) {
        pessoa.id[0] = read_ext_eeprom(local);
        local++;
        pessoa.id[1] = read_ext_eeprom(local);
        local++;
        pessoa.senha[0] = read_ext_eeprom(local);
        local++;
        pessoa.senha[1] = read_ext_eeprom(local);
        local++;
        pessoa.senha[2] = read_ext_eeprom(local);
        local++;
        pessoa.senha[3] = read_ext_eeprom(local);
        local++;
        pessoa.status = read_ext_eeprom(local);
        local++;
        printf(lcd_escreve, "\fID: %c%c Stats: %c", pessoa.id[0], pessoa.id[1], pessoa.status);
        printf(lcd_escreve, "\nSenha: %c%c%c%c", pessoa.senha[0], pessoa.senha[1], pessoa.senha[2], pessoa.senha[3]);
        while (f != 1) {
          tecla = tc_tecla(1500);
          if (tecla == '#') {
            f = 1;
          }
        }
        k = 1;
      } else {
        positionFindId += 7;
        if (positionFindId > 73) {
          printf(lcd_escreve, "\fID NAO ENCONTRADO: \n");
          delay_ms(1500);
          k = 1;
        }
      }
    } else {
      positionFindId += 7;
      if (positionFindId > 73) {
        printf(lcd_escreve, "\fID NAO ENCONTRADO: \n");
        delay_ms(1500);
        k = 1;
      }
    }
  }
}
void cadastraCliente() {
    int aux = 0, f = 0, cont = 0, y = 0;
    char tcl, aux1;
    int lastMemoryPosition = 7;
    // aqui ele pede o cadastro do adm e grava na primeira posi??o da memoria
    if (flagAdm == 0) {
      printf(lcd_escreve, "\fCADASTRAR ADM\n");
      delay_ms(1500);
      printf(lcd_escreve, "\fDIGITE O ID: \n");
      while (aux < 2) {
        // fica esperando o usuario digitar algo
        tcl = tc_tecla(1500);
        while (y == 0) {
          if (tcl == '@') {
            tcl = tc_tecla(1500);
          } else {
            y = 1;
          }
        }
        // acaba aqui         
        printf(lcd_escreve, "%c", tcl);
        Adminstrador.id[aux] = tcl;
        aux++;
      }
      while (f != 1) {
        aux1 = tc_tecla(1500);
        if (aux1 == '#') {
          f = 1;
        }
      }
      f = 0;
      aux1 = ' ';
      aux = 0;
      y = 0;
      printf(lcd_escreve, "\fDIGITE A SENHA: \n");
      while (aux < 4) {
        // fica esperando o usuario digitar algo
        tcl = tc_tecla(1500);
        while (y == 0) {
          if (tcl == '@') {
            tcl = tc_tecla(1500);
          } else {
            y = 1;
          }
        }
        // acaba aqui   
        printf(lcd_escreve, "%c", tcl);
        Adminstrador.senha[aux] = tcl;
        aux++;
      }
      while (f != 1) {
        aux1 = tc_tecla(1500);
        if (aux1 == '#') {
          f = 1;
        }
      }
      f = 0;
      y = 0;
      printf(lcd_escreve, "\fDIGITE O STATUS: \n");
      // fica esperando o usuario digitar algo
      tcl = tc_tecla(1500);
      while (y == 0) {
        if (tcl == '@') {
          tcl = tc_tecla(1500);
        } else {
          y = 1;
        }
      }
      // acaba aqui   
      y = 0;
      printf(lcd_escreve, "%c", tcl);
      Adminstrador.status = tcl;
      while (f != 1) {
        tecla = tc_tecla(1500);
        if (tecla == '#') {
          f = 1;
        }
      }
      printf(lcd_escreve, "\fCadastrando");
      delay_ms(1500);
      write_ext_eeprom(0, Adminstrador.id[0]);
      write_ext_eeprom(1, Adminstrador.id[1]);
      write_ext_eeprom(2, Adminstrador.senha[0]);
      write_ext_eeprom(3, Adminstrador.senha[1]);
      write_ext_eeprom(4, Adminstrador.senha[2]);
      write_ext_eeprom(5, Adminstrador.senha[3]);
      write_ext_eeprom(6, Adminstrador.status);
      flagAdm = 1;
      printf(lcd_escreve, "\fCadastrado");
      delay_ms(1500);
    } else {
      // aqui s?o todos os outros clientes que s?o cadastrados
      printf(lcd_escreve, "\fDIGITE O ID: \n");
      while (tecla != '#' && aux < 2) {
        // fica esperando o usuario digitar algo
        tcl = tc_tecla(1500);
        while (y == 0) {
          if (tcl == '@') {
            tcl = tc_tecla(1500);
          } else {
            y = 1;
          }
        }
        // acaba aqui   
        printf(lcd_escreve, "%c", tcl);
        pessoa.id[aux] = tcl;
        aux++;
      }
      while (f != 1) {
        aux1 = tc_tecla(1500);
        if (aux1 == '#') {
          f = 1;
        }
      }
      f = 0;
      aux1 = ' ';
      aux = 0;
      y = 0;
      printf(lcd_escreve, "\fDIGITE A SENHA: \n");
      while (aux < 4) {
        // fica esperando o usuario digitar algo
        tcl = tc_tecla(1500);
        while (y == 0) {
          if (tcl == '@') {
            tcl = tc_tecla(1500);
          } else {
            y = 1;
          }
        }
        // acaba aqui  
        printf(lcd_escreve, "%c", tcl);
        pessoa.senha[aux] = tcl;
        aux++;
      }
      while (f != 1) {
        aux1 = tc_tecla(1500);
        if (aux1 == '#') {
          f = 1;
        }
      }
      f = 0;
      y = 0;
      printf(lcd_escreve, "\fDIGITE O STATUS: \n");
      // fica esperando o usuario digitar algo
      tcl = tc_tecla(1500);
      while (y == 0) {
        if (tcl == '@') {
          tcl = tc_tecla(1500);
        } else {
          y = 1;
        }
      }
      // acaba aqui   
      y = 0;
      printf(lcd_escreve, "%c", tcl);
      pessoa.status = tcl;
      while (f != 1) {
        tecla = tc_tecla(1500);
        if (tecla == '#') {
          f = 1;
        }
      }
      printf(lcd_escreve, "\fCadastrando");
      delay_ms(1500);
      // varrer procurando posi??o vazia, o 73 ? 512/7 que ? o numero de pessoas total que cabem na memoria
      while (cont < 73) {
        if (read_ext_eeprom(lastMemoryPosition) == 0xff) {

          write_ext_eeprom(lastMemoryPosition, pessoa.id[0]);
          lastMemoryPosition++;
          write_ext_eeprom(lastMemoryPosition, pessoa.id[1]);
          lastMemoryPosition++;
          write_ext_eeprom(lastMemoryPosition, pessoa.senha[0]);
          lastMemoryPosition++;
          write_ext_eeprom(lastMemoryPosition, pessoa.senha[1]);
          lastMemoryPosition++;
          write_ext_eeprom(lastMemoryPosition, pessoa.senha[2]);
          lastMemoryPosition++;
          write_ext_eeprom(lastMemoryPosition, pessoa.senha[3]);
          lastMemoryPosition++;
          write_ext_eeprom(lastMemoryPosition, pessoa.status);
          lastMemoryPosition++;
          cont = 75;
        } else {
          lastMemoryPosition += 7;
        }
        cont++;
      }
      printf(lcd_escreve, "\fCadastrado");
      delay_ms(1500);
    }
}

//enviar do piC para o banco, vai receber uma flag no serial para come?ar a enviar,
void atualizarBancoSQL() {
int positionFindId = 0;
int local;
int k = 0;
printf(lcd_escreve, "\fENVIANDO");
while (k != 1) {
        local = positionFindId;
        pessoa.id[0] = read_ext_eeprom(local);
        local++;
        pessoa.id[1] = read_ext_eeprom(local);
        local++;
        pessoa.senha[0] = read_ext_eeprom(local);
        local++;
        pessoa.senha[1] = read_ext_eeprom(local);
        local++;
        pessoa.senha[2] = read_ext_eeprom(local);
        local++;
        pessoa.senha[3] = read_ext_eeprom(local);
        local++;
        pessoa.status = read_ext_eeprom(local);
        local++;
        
        fprintf(wireless,"%c%c",pessoa.id[0],pessoa.id[1]);
        fprintf(wireless,"%c%c%c%c",pessoa.senha[0],pessoa.senha[1],pessoa.senha[2],pessoa.senha[3]);
        fprintf(wireless,"%c",pessoa.status);
        positionFindId +=7;
        
      if (positionFindId > 73) {
          fprintf(wireless,"!");
          k = 1;
        }
}
delay_ms(1500);
}

void atualizarBancoPIC(){
lcd_escreve("\fAtualizando...\n");

while (recebendo_dados) {

  if (dado == '\n') {
    recebendo_dados = FALSE;    
  }
  
  write_ext_eeprom(lastMemoryPosition++, dado);
  
}
}

