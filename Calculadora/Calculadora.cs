using System;

namespace CalculadoraApp {

  // Class responsável pela limpesa do ecrã e colocação da Banner de Apresentação.
  public class Apresentaçao{
    public void Banner(){
      Console.Clear(); //Limpar Ecrã.
      Console.WriteLine("##################################################################################################################");
      Console.WriteLine("\t\t\t\t\tBem Vindo á minha Calculadora!");
      Console.WriteLine("\t\t\tProjeto desenvolvido no ambito do Módulo PETD para o ISTEC.");
      Console.WriteLine("\t\tTodo o Código deste utilitário foi escrito e testado por: Daniel Vaz.nº30183");
      Console.WriteLine("##################################################################################################################");
      Console.WriteLine("");
    }
  }

  // Class que armazena metodos relacionados com Operações e Contas.
  public class Operaçoes {

    public string EscolhaOper(){
      string oper="";
      while(oper==""){
        Console.WriteLine("\t\t\tQue Operação é que pretende efetuar ?");
        Console.WriteLine("\t\t[ +(soma) ; -(subtração) ; *(multiplicação) ; /(divisão) ]");
        oper = Console.ReadLine();
        if(oper == "+" || oper == "-" || oper == "*" || oper == "/"){
          break;
        }
        else{
          Console.WriteLine("Introduza um operador válido! (Tem de introduzir o seu respetivo simbolo)");
          oper="";
        }
      }
      return oper;
    } // Fim do metodo EscolhaOper().


    public double Conta(string oper, double num1, double num2){
      double result=0;
      switch(oper){
        case "+":
          result=num1+num2;
          break;
        case "-":
          result=num1-num2;
          break;
        case "*":
          result=num1*num2;
          break;
        case "/":
          result=num1/num2;
          break;
      }
      return result;
    } // Fim do metodo Conta().
  } //Fim da Class Operaçoes.


  //Class Principal.
  public class Calculadora
  {
    static public void Main ()
      {
        // Criação de variaveis globais.
        bool off=false;
        bool primeiraVez=true;
        bool check;
        double num1=0, num2=0, result=0;
        string num1t, num2t;
        string oper="";
        //Instanciamento das Classes necessárias.
        Operaçoes x = new Operaçoes();
        Apresentaçao y = new Apresentaçao();

        // Limpesa de Ecrã e colocação da Banner.
        y.Banner();

        // Loop infinito responsável por obter e calcular valores até que seja dito em contrário pelo próprio Utilizador.
        while(!off){

          if(primeiraVez){
            //Obtenção de valores.
            while(num1 == 0){
              Console.Write("Introduza o Primeiro Número: ");
              num1t = Console.ReadLine();
              check = true;
              try {
                Convert.ToDouble(num1t);
              }
              catch {
                Console.WriteLine("Não introduzio um número válido!");
                check = false;
              }
              if(check){
                num1 = Convert.ToDouble(num1t);
              }
            }
            while(num2 == 0){
              Console.Write("Introduza o Segundo Número: ");
              num2t = Console.ReadLine();
              check = true;
              try {
                Convert.ToDouble(num2t);
              }
              catch {
                Console.WriteLine("Não introduzio um número válido!");
                check = false;
              }
              if(check){
                num2 = Convert.ToDouble(num2t);
              }
            }


            y.Banner();
            // Evocação do método EscolhaOper() para se saber que operador usar na Conta().
            oper = x.EscolhaOper();
            // Evocação do método Conta() para ser obtido o resultado.
            result = x.Conta(oper, num1, num2);

            // Apresentação de Resultado da conta.
            y.Banner();
            Console.WriteLine("Resultado:");
            Console.WriteLine("\t\t\t\t{0} {1} {2} = {3}", num1, oper, num2, result);
            Console.WriteLine("");  // Formatação de texto de forma a ficar mais compreensivel.

            primeiraVez=false;
          }

          // Else caso não seja a primeira conta que está a ser efetuada pela calculadora.
          // Este else apenas existe para que a calculadora saiba fazer a passagem do resultado de uma conta feita anteriormente,
          // para o primeiro valor a ser usado na próxiam conta.
          else{
            Console.WriteLine("Deseja Prosseguir com os calculos (S/N)?");
            string opc = Console.ReadLine();

            y.Banner();
            if (opc == "S" || opc == "s"){
              num1 = result; // Resultado da Conta anterior é agora o nosso primeiro valor "num1". Não sendo assim novamente solicitado ao utilizador.
              oper = x.EscolhaOper();
              num2 = 0;
              while(num2==0){
                Console.Write("Introduza o Segundo Número: ");
                num2t = Console.ReadLine();
                check = true;
                try {
                  Convert.ToDouble(num2t);
                }
                catch {
                  Console.WriteLine("Não introduzio um número válido!");
                  check = false;
                }
                if(check){
                  num2 = Convert.ToDouble(num2t);
                }
              }

              result = x.Conta(oper, num1, num2);

              y.Banner();
              Console.WriteLine("Resultado:");
              Console.WriteLine("\t\t\t\t{0} {1} {2} = {3}", num1, oper, num2, result);
              Console.WriteLine("");  // Formatação de texto de forma a ficar mais compreensivel.
            }

            else if (opc == "N" || opc == "n"){
              Console.WriteLine("");
              Console.WriteLine("\t\t\tObrigado por ter usado a Calculadora!");
              off = true; // Alteração de boolean para se sair do while loop infinito.
              Console.ReadLine();
            }
            else{
              Console.WriteLine("");
              Console.WriteLine("\t\tNão foi escolhida uma opção válida! A calculadora irá terminar. ");
              off = true;
              Console.ReadLine();
            }
          }

        }// Fim do While loop
      }// Fim do Main
  }// Fim da Class Calculadora
}//Fim do NameSpace
