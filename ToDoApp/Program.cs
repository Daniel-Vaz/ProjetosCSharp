using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TodoList
{
    class Program
    {
        
        //Classes Modelo para Converssão de Json's para Objetos.
        //Associado a cada class está o respetivo ficheiro PHP que vai retornar os dados no formato de Json.
        //  Class Modelo para resultados do ficheiro loginUser.php
        public class Modelo_usersLogin {
            public string success { get; set; }
            public string user_id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
        }

        //  Class Modelo para resultado do ficheiro getUsers.php 
        public class Modelo_getUsers {
            public string user_id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
        }

        //  Class Modelo para resultado do ficheiro getTarefas.php
        public class Modelo_Tarefas {
            public int id { get; set; }
            public int dono { get; set; }
            public string titulo { get; set; }
            public string data { get; set; }
            public string descricao { get; set; }
        }

        // Class que armazena todos os métodos que teem contacto com a Base de Dados.
        public class MetodosBD
        {
            // Método Registar:
            //  Usado para registar um novo utilizador na nossa Base de Dados.
            public async Task<string> Registar(string username, string password)
            {
                const string Url = "http://todoaplicacao.000webhostapp.com/phpScripts/registerUser.php";
                HttpClient httpClient = new HttpClient();

                var values = new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                };

                var content = new FormUrlEncodedContent(values);
                var response = httpClient.PostAsync(Url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else { return "Erro a Establecer Comunicação com o Servidor."; }
            }

            //Método Login: 
            //  Usado para quando queremos fazer login de um Utilizador.
            //  Contacta a BD para confirmar se o utilizador existe ou não.
            public async Task<string> Login(string username, string password)
            {
                const string Url = "http://todoaplicacao.000webhostapp.com/phpScripts/loginUser.php";
                HttpClient httpClient = new HttpClient();

                var values = new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                };

                var content = new FormUrlEncodedContent(values);
                var response = httpClient.PostAsync(Url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else { return "Erro a Establecer Comunicação com o Servidor."; }
            }

            //Método UserID:
            //  Usado para obtermos o userID do utilizador que acabou de fazer Login
            public async Task<string> UserID(string username) {
                const string Url = "http://todoaplicacao.000webhostapp.com/phpScripts/getUsers.php";
                HttpClient httpclient = new HttpClient();

                var values = new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string>("username", username),
                };

                var content = new FormUrlEncodedContent(values);
                var response = httpclient.PostAsync(Url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else { return "Erro a Establecer Comunicação com o Servidor."; }
            }

            //Método Tarefas:
            //  Usado para obter todas as tarefas existentes na Base de Dados, relacionados com um respetivo userID.
            public async Task<string> Tarefas(string userID) {
                const string Url = "http://todoaplicacao.000webhostapp.com/phpScripts/getTarefas.php";
                HttpClient httpclient = new HttpClient();

                var values = new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string>("userID", userID),
                };

                var content = new FormUrlEncodedContent(values);
                var response = httpclient.PostAsync(Url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else { return "Erro a Establecer Comunicação com o Servidor."; }
            }

            //Método criarTarefa:
            //  É usado para criar um registo de uma Tarefa na Base de Dados.
            //  A nova tarefa deverá ficar associada ao utilizador que efetuou login.
            public async Task<string> criarTarefa(string userID, string titulo, string data, string descricao) {
                const string Url = "http://todoaplicacao.000webhostapp.com/phpScripts/criarTarefa.php";
                HttpClient httpclient = new HttpClient();

                var values = new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string>("userID", userID),
                    new KeyValuePair<string, string>("titulo", titulo),
                    new KeyValuePair<string, string>("data", data),
                    new KeyValuePair<string, string>("descricao", descricao),
                };

                var content = new FormUrlEncodedContent(values);
                var response = httpclient.PostAsync(Url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else { return "Erro a Establecer Comunicação com o Servidor."; }
            }

            //Método apagarTarefa:
            //  Usado para Eleminar o registo de uma determinada tarefa indicada pelo utilizador.
            //  O User logado apenas conssegue manipular as tarefas que a ele lhe dizem respeito.
            public async Task<string> apagarTarefa(string tarefaID)
            {
                const string Url = "http://todoaplicacao.000webhostapp.com/phpScripts/apagarTarefa.php";
                HttpClient httpclient = new HttpClient();

                var values = new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string>("tarefaID", tarefaID)
                };

                var content = new FormUrlEncodedContent(values);
                var response = httpclient.PostAsync(Url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else { return "Erro a Establecer Comunicação com o Servidor."; }
            }

            //Método editarTarefa:
            // Usado quando queremos editar o registo de uma determinada tarefa na nossa Base de Dados. 
            public async Task<string> editarTarefa(string tarefaID, string titulo, string data, string descricao)
            {
                const string Url = "http://todoaplicacao.000webhostapp.com/phpScripts/editarTarefa.php";
                HttpClient httpclient = new HttpClient();

                var values = new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string>("id", tarefaID),
                    new KeyValuePair<string, string>("titulo", titulo),
                    new KeyValuePair<string, string>("data", data),
                    new KeyValuePair<string, string>("descricao", descricao),
                };

                var content = new FormUrlEncodedContent(values);
                var response = httpclient.PostAsync(Url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else { return "Erro a Establecer Comunicação com o Servidor."; }
            }
            

        }

        // Class que armazena todos os metodos utilitários.
        //  Aqui encontramos todos os métodos que permitem chamar os outros métodos async que contactão coma Base de Dados;
        //  Também vamos ver os restantes métodos que permitem apenas efectuar pequenas e determinadas ações, 
        //  que permitem o correto funcionamento do programa (Verificação de erros, Correções estéticas, etc)
        public class MetodosAjuda
        {

            //Método banner
            //  Apenas Limpa e compoem o topo de ecrã de forma ao mesmo ficar mais presentável.
            public void Banner()
            {
                Console.Clear();
                Console.WriteLine("#######################################################################################################################");
                Console.WriteLine("\t\t\t\tBem Vindo ao meu Gestor de Tarefas.");
                Console.WriteLine("\t\t\tPrograma desenvolvido no ambito do Módulo POO do ISTEC.");
                Console.WriteLine("\tEste programa comunica com uma Base de Dados MySQl para gerir as tarefas entre diferentes utilizadores.");
                Console.WriteLine("\t\t\tTodo o código aqui usado foi escrito e testado por: Daniel Vaz nº30183");
                Console.WriteLine("#######################################################################################################################");
                Console.WriteLine("");
            }

            // Método de Converssão de String para Hash de base SHA256
            public String sha256_hash(String value)
            {
                StringBuilder Sb = new StringBuilder();

                using (SHA256 hash = SHA256Managed.Create())
                {
                    Encoding enc = Encoding.UTF8;
                    Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                    foreach (Byte b in result)
                        Sb.Append(b.ToString("x2"));
                }
                return Sb.ToString();
            }

            // Este método executa o método acima Registar().
            //   Confirma se a ação foi executada com sucesso ou não,
            //   ou se já existe algum user com as dados fornecidos.
            public async void ExecutarRegistar(string user, string pass)
            {
                MetodosBD MBD = new MetodosBD();
                var tarefa = MBD.Registar(user, pass);
                string resultado = await tarefa;
                if (Int32.Parse(resultado) == 1)
                {
                    Console.WriteLine("Os Dados Introduzidos já existem em sistema.");
                }
                else if (Int32.Parse(resultado) == 0)
                {
                    Console.WriteLine("O Utilizador foi criado com Sucesso.");
                }
                else {
                    Console.WriteLine("Ocorreu um erro aquando criação da conta. Tente Novamente mais tarde.");
                }
            }

            // Este método executa o método Login() acima indicado. 
            //  Retorna um valor true ou false pendente se os dados indicados,
            //  estão corretos ou enrrados.
            public async Task<string> ExecutarLogin(string user, string pass)
            {
                MetodosBD MBD = new MetodosBD();
                var tarefa = MBD.Login(user, pass);
                var resultado = await tarefa;
                Modelo_usersLogin resultado_Traduzido = JsonConvert.DeserializeObject<Modelo_usersLogin>(resultado);
                return resultado_Traduzido.success;
            }

            // Este método apenas executa o método UserID() acima indicado.
            //  Retorna o respetivo id do utilizador que efectuou login.
            public async Task<string> ExecutarUserId(string user) {
                MetodosBD MBD = new MetodosBD();
                var tarefa = MBD.UserID(user);
                var resultado = await tarefa;
                Modelo_getUsers resultado_Traduzido = JsonConvert.DeserializeObject<Modelo_getUsers>(resultado);
                return resultado_Traduzido.user_id;
            }

            // Este método executa o método Tarefas() acima exposto.
            //  Apresenta no ecrã todas as tarefas que existem associados ao utilizador Logado.
            public async void ExecutarTarefas(string userID)
            {
                MetodosBD MBD = new MetodosBD();
                var tarefa = MBD.Tarefas(userID);
                var resultado = await tarefa;
                List<Modelo_Tarefas> resultado_Traduzido = JsonConvert.DeserializeObject<List<Modelo_Tarefas>>(resultado);
                Console.WriteLine("==============================================================");
                foreach (var i in resultado_Traduzido) {
                    Console.WriteLine("\t\t\t[{0}]", i.id);
                    Console.WriteLine("Titulo: {0}", i.titulo);
                    Console.WriteLine("Data: {0}", i.data);
                    Console.WriteLine("Descrição: {0}", i.descricao);
                    Console.WriteLine("==============================================================");
                }
            }

            // Este método executa o método criarTarefa() acima indicado.
            //  Obtem do utilizador os campos necessários para a criação de uma nova tarefa,
            //  e envia os mesmo para a Base de Dados. 
            public async void ExecutarCriacao(string userID)
            {
                Banner();
                Console.WriteLine("Para a Criação da Tarefa necessita de responder as perguntas abaixo:");
                Console.WriteLine("");
                Console.WriteLine("Qual o titulo da Tarefa a Criar?");
                Console.Write("## ");
                string novaTarefaTitulo = Console.ReadLine();
                Console.WriteLine("Qual a data para a Tarefa a Criar? (Tem de ser no seguinte formato: AAAA-MM-DD)");
                Console.Write("## ");
                string novaTarefaData = Console.ReadLine();
                Console.WriteLine("Qual a descrição da Tarefa a Criar?");
                Console.Write("## ");
                string novaTarefaDescricao = Console.ReadLine();

                MetodosBD MBD = new MetodosBD();
                var tarefa = MBD.criarTarefa(userID, novaTarefaTitulo, novaTarefaData, novaTarefaDescricao);
                var resultado = await tarefa;
                if(Int32.Parse(resultado) == 0)
                {
                    Banner();
                    Console.WriteLine("Tarefa criada com Sucesso.");
                    Console.ReadLine();
                }
                else
                {
                    Banner();
                    Console.WriteLine("Ocorreu um erro aquando criação da Tarefa. Tente novamente mais tarde.");
                    Console.ReadLine();
                }
            }

            // Este método executa o método acima indicado chamado apagarTarefa().
            //  Pendente o ID da tarefa fornecido pelo utilizador, comunicamos com a Base de Dados,
            //  e eliminamos a respetiva entrada com o mesmo ID. 
            public async void ExecutarApagar(string tarefaID)
            {
                MetodosBD MDB = new MetodosBD();
                var tarefa = MDB.apagarTarefa(tarefaID);
                var resultado = await tarefa;
                if (Int32.Parse(resultado) == 0)
                {
                    Banner();
                    Console.WriteLine("Tarefa apagada com Sucesso.");
                    Console.ReadLine();
                }
                else
                {
                    Banner();
                    Console.WriteLine("Ocorreu um erro aquando eleminação da Tarefa. Tente novamente mais tarde.");
                    Console.ReadLine();
                }
            }

            // Este método executa o método acima exposto chamado editarTarefa().
            //  Este método é responsável por ir questionando ao utilizador quais os compos que este pretende alterar de uma determinada tarefa.
            //  Apenas após todas as alterações estarem efetuadas pelo utilizador, é que este método comunica com a Base de Dados, 
            //  para alterar os respetivos campos. 
            public async void ExecutarEditar(string tarefaID, string userID)
            {
                MetodosBD MBD = new MetodosBD();
                var tarefa = MBD.Tarefas(userID);
                var resultado = await tarefa;
                List<Modelo_Tarefas> resultado_Traduzido = JsonConvert.DeserializeObject<List<Modelo_Tarefas>>(resultado);
                foreach (var i in resultado_Traduzido)
                {
                    if (i.id == Int32.Parse(tarefaID)) {
                        bool fimAlteracoes = false;
                        string titulo = "";
                        string data = "";
                        string descricao = "";
                        
                        titulo = i.titulo;
                        data = i.data;
                        descricao = i.descricao;

                        while (!fimAlteracoes)
                        {
                            Banner();
                            Console.WriteLine("Que Campo é que pretende alterar?");
                            Console.WriteLine("[1]- Titulo;");
                            Console.WriteLine("[2]- Data;");
                            Console.WriteLine("[3]- Descrição;");
                            Console.WriteLine("[4]- Terminei alterações.");
                            Console.Write("## ");
                            int escolha = Int32.Parse(Console.ReadLine());
                            
                            switch (escolha)
                            {
                                case 1:
                                    Console.Write("Introduza o novo Titulo: ");
                                    titulo = Console.ReadLine();
                                    break;
                                case 2:
                                    Console.Write("Introduza a nova Data: ");
                                    data = Console.ReadLine();
                                    break;
                                case 3:
                                    Console.Write("Introduza a nova Descrição: ");
                                    descricao = Console.ReadLine();
                                    break;
                                case 4:
                                    fimAlteracoes = true;
                                    break;
                                default:
                                    Banner();
                                    fimAlteracoes = false;
                                    Console.WriteLine("Não escolheu uma opção válida.");
                                    break;
                            }
                        }

                        var tarefa2 = MBD.editarTarefa(tarefaID, titulo, data, descricao);
                        var resultado2 = await tarefa2;
                        if (Int32.Parse(resultado2) == 0)
                        {
                            Banner();
                            Console.WriteLine("Tarefa editada com Sucesso.");
                            Console.ReadLine();
                        }
                        else
                        {
                            Banner();
                            Console.WriteLine("Ocorreu um erro aquando alteração da Tarefa. Tente novamente mais tarde.");
                            Console.ReadLine();
                        }
                    }
                }
            }

            // Método NumCheck.
            //  Este método é chamado sempre que queremos confirmar se é possivel de transformar uma determinada,
            //  string em um int. Retorna true or false pendente o seu sucesso. 
            public bool NumCheck(string str) {
                return int.TryParse(str, out int num);
            }

        }
        
        // Método Main.
        //  Ponto de partida da execução do nosso programa. 
        //  Toma recursso de todos os outros métodos e classes indicados acima de forma a trabalhar os dados, e apresentar os mesmo ao utilizador. 
        //  Segue uma lógica de Loops que "prendem" o user em menus de escolha de ações, onde este pode a qualquer isntante sair. 
        static void Main(string[] args)
        {
            MetodosAjuda MA = new MetodosAjuda();
            MetodosBD MBD = new MetodosBD();

            bool loopPrimario = true;
            bool loopLogin = false;
            while (loopPrimario)
            {
                MA.Banner();
                Console.WriteLine("O que pretende fazer?");
                Console.WriteLine("----------------------");
                Console.WriteLine("[1] Login");
                Console.WriteLine("[2] Registar");
                Console.WriteLine("[3] Sair");
                Console.Write("## ");

                string escolhaMenuInicial = Console.ReadLine();
                int escolhaMenuInicialINT = 0;
                if (MA.NumCheck(escolhaMenuInicial))
                {
                    escolhaMenuInicialINT = Int32.Parse(escolhaMenuInicial);
                }

                switch (escolhaMenuInicialINT)
                {
                    case 1:
                        MA.Banner();
                        Console.WriteLine("Introduza os seus dados de acesso:");
                        Console.WriteLine("-----------------------------------");
                        Console.Write("Username: ");
                        string user = Console.ReadLine();
                        Console.Write("Password: ");
                        string pass = MA.sha256_hash(Console.ReadLine());
                        Task<string> resposta = Task.Run(async () => await MA.ExecutarLogin(user, pass));
                        resposta.Wait();
                        if (resposta.Result == "true") {
                            loopLogin = true;
                            Task<string> utilizadorID = Task.Run(async () => await MA.ExecutarUserId(user));
                            utilizadorID.Wait();
                            var userID = utilizadorID.Result;
                            while (loopLogin)
                            {
                                MA.Banner();
                                Console.WriteLine("==============================================================");
                                Console.WriteLine("|\t\t\tAs tuas tarefas\t\t\t     |");
                                Console.WriteLine("|\t\t\t\t\t\t\t     |");
                                MA.ExecutarTarefas(userID);
                                Console.WriteLine("");
                                Console.WriteLine("Que ações é que pretende executar?");
                                Console.WriteLine("----------------------------------");
                                Console.WriteLine("[1]- Criar Tarefa;");
                                Console.WriteLine("[2]- Editar Tarefa;");
                                Console.WriteLine("[3]- Apagar Tarefa;");
                                Console.WriteLine("[4]- Fazer LogOff;");
                                Console.Write("## ");

                                int escolhaMenuAcoesINT = 0;
                                string escolhaMenuAcoes = Console.ReadLine();
                                if (MA.NumCheck(escolhaMenuAcoes))
                                {
                                    escolhaMenuAcoesINT = Int32.Parse(escolhaMenuAcoes);
                                }

                                switch (escolhaMenuAcoesINT)
                                {
                                    case 1:
                                        MA.Banner();
                                        MA.ExecutarCriacao(userID);
                                        break;
                                    case 2:
                                        MA.Banner();
                                        MA.ExecutarTarefas(userID);
                                        Console.WriteLine("");
                                        Console.WriteLine("Introduza o ID da Tarefa que pretende Editar:");
                                        Console.WriteLine("---------------------------------------------");
                                        Console.Write("## ");
                                        string idTarefaEditar = Console.ReadLine();
                                        MA.ExecutarEditar(idTarefaEditar, userID);
                                        break;
                                    case 3:
                                        MA.Banner();
                                        MA.ExecutarTarefas(userID);
                                        Console.WriteLine("");
                                        Console.WriteLine("Introduza o ID da Tarefa que pretende Apagar:");
                                        Console.WriteLine("---------------------------------------------");
                                        Console.Write("## ");
                                        string idTarefaApagar = Console.ReadLine();
                                        MA.ExecutarApagar(idTarefaApagar);
                                        break;
                                    case 4:
                                        loopLogin = false;
                                        break;
                                    default:
                                        MA.Banner();
                                        Console.WriteLine("Não escolheu uma opção válida.");
                                        Console.ReadLine();
                                        loopLogin = true;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            MA.Banner();
                            Console.WriteLine("Os Dados introduzidos não estão Corretos!");
                            Console.ReadLine();
                        }
                        break;
                    case 2:
                        MA.Banner();
                        Console.WriteLine("Escolha os dados que pretende Registar: ");
                        Console.WriteLine("---------------------------------------");
                        Console.Write("Username: ");
                        string novoUser = Console.ReadLine();
                        Console.Write("Password: ");
                        string novaPass = MA.sha256_hash(Console.ReadLine());
                        MA.Banner();
                        MA.ExecutarRegistar(novoUser, novaPass);                       
                        Console.ReadLine();
                        break;
                    case 3:
                        MA.Banner();
                        Console.WriteLine("\t\t\tObrigado pela Sua Utilização.");
                        Console.ReadLine();
                        loopPrimario = false;
                        Environment.Exit(0);
                        break;
                    default:
                        MA.Banner();
                        Console.WriteLine("Não escolheu um opção válida");
                        Console.ReadLine();
                        loopPrimario = true;
                        break;
                }
            } // Fim do while
        } // Fim do Main
    }
}

