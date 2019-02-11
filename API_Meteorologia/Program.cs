using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    class Program
    {
        // Urls dos diferentes ficheiros a pedir à API.
        //  Os primeiros 3 servem para obter os dados gerais dos diferentes locais do Pais.
        //  os ultimos 3 são para "traduzir" os ID's presentes nos dados principais, para strings escritos em Portugues.
        const string url1 = "http://api.ipma.pt/open-data/forecast/meteorology/cities/daily/hp-daily-forecast-day0.json";
        const string url2 = "http://api.ipma.pt/open-data/forecast/meteorology/cities/daily/hp-daily-forecast-day1.json";
        const string url3 = "http://api.ipma.pt/open-data/forecast/meteorology/cities/daily/hp-daily-forecast-day2.json";
        const string urlLocalidades = "http://api.ipma.pt/open-data/distrits-islands.json";
        const string urlTempo = "http://api.ipma.pt/open-data/weather-type-classe.json";
        const string urlVento = "http://api.ipma.pt/open-data/wind-speed-daily-classe.json";


        // As Seguintes 8 Classes são para criarmos os Objectos que vão servir como Modelos,
        // dos diferentes dados obtidos pela API.

        //      Classes Modelo para Tipos de Tempos.
        public class DadosTempo
        {
            public string descIdWeatherTypeEN { get; set; }
            public string descIdWeatherTypePT { get; set; }
            public int idWeatherType { get; set; }
        }
        public class ModeloTempo
        {
            public string owner { get; set; }
            public string country { get; set; }
            public List<DadosTempo> data { get; set; }
        }


        //      Classes Modelo para Ventos.
        public class DadosVento
        {
            public string descClassWindSpeedDailyEN { get; set; }
            public string descClassWindSpeedDailyPT { get; set; }
            public int classWindSpeed { get; set; }
        }
        public class ModeloVentos
        {
            public string owner { get; set; }
            public string country { get; set; }
            public List<DadosVento> data { get; set; }
        }


        //      Classes Modelo para Localidades.
        public class Registos
        {
            public int idRegiao { get; set; }
            public string idAreaAviso { get; set; }
            public int idConcelho { get; set; }
            public int globalIdLocal { get; set; }
            public string latitude { get; set; }
            public int idDistrito { get; set; }
            public string local { get; set; }
            public string longitude { get; set; }
        }
        public class Localidades
        {
            public string owner { get; set; }
            public string country { get; set; }
            public List<Registos> data { get; set; }
        }


        //      Classes Modelo Para Valores.
        public class Dados
        {
            public float precipitaProb { get; set; }
            public int tMin { get; set; }
            public int tMax { get; set; }
            public string predWindDir { get; set; }
            public int idWeatherType { get; set; }
            public int classWindSpeed { get; set; }
            public string longitude { get; set; }
            public int globalIdLocal { get; set; }
            public string latitude { get; set; }
            public int? classPrecInt { get; set; }
        }
        public class Modelo
        {
            public string owner { get; set; }
            public string country { get; set; }
            public string forecastDate { get; set; }
            public List<Dados> data { get; set; }
            public string dataUpdate { get; set; }
        }


        // Class de armazenamento de todos os Métodos.
        // Esta class irá armazenar os diferentes métodos que serão usados ao longo da execução do Programa.
        public class Funcoes
        {
            // O Método "Banner()" serve apenas propósitos estéticos, sendo evocado para por o terminal mais apelativo.
            public void Banner()
            {
                Console.Clear(); //Limpar Ecrã.
                Console.WriteLine("##################################################################################################################");
                Console.WriteLine("\t\t\t\t\tBem Vindo ao Meu Previsor de Meteorologia!");
                Console.WriteLine("\t\t\t\tProjeto desenvolvido no ambito do Módulo POO para o ISTEC.");
                Console.WriteLine("\t\t\tTradutor da API do IPMA-Instituo Portugues da Meteorologia e Atmosfera");
                Console.WriteLine("\t\t\tTodo o Código deste utilitário foi escrito e testado por: Daniel Vaz.nº30183");
                Console.WriteLine("##################################################################################################################");
                Console.WriteLine("");
            }

            // O Método "OpcoesCheck()" serve para retornar um valor bollean informando o resto do programa,
            // se o string que lhe foi passado pode ser de forma segura convertido para int, ou não.
            public bool OpcoesCheck(string temp_opc)
            {
                return int.TryParse(temp_opc, out int opc);
            }

            // O Método "Opcao1()" é chamado caso o utilizador queira obter as Temperaturas Min\Max de todas as Localidades.
            //  - Após Comunicar com o respetivo url, irá obter o resultado da API das Localidades e Dados Gerais.
            //  - Vai organizar os dados e apresentar os mesmos ao utilizador.
            public async Task Opcao1(string url)
            {
                Funcoes f = new Funcoes();
                Console.Clear();
                f.Banner();
                HttpClient cliente = new HttpClient();
                var conteudo = await cliente.GetStringAsync(url); // Obter Dados Gerais.
                Modelo valores = JsonConvert.DeserializeObject<Modelo>(conteudo);
                var localidades = await cliente.GetStringAsync(urlLocalidades); // Obter Localidades.
                Localidades registos = JsonConvert.DeserializeObject<Localidades>(localidades);

                // Abaixo segue-se toda aapresentação dos daos ao utilizador.
                Console.WriteLine("======================");
                foreach (var i in valores.data)
                {
                    foreach (var i2 in registos.data)
                    {
                        if (i2.globalIdLocal == i.globalIdLocal)
                        {
                            Console.WriteLine("ID_Localidade: {0}", i2.local);
                        }
                    }
                    Console.WriteLine("Temperatura Min.: {0}º", i.tMin);
                    Console.WriteLine("Temperatura Max.: {0}º", i.tMax);
                    Console.WriteLine("======================");
                }
                Console.ReadLine(); // Reter a conssola para dar tempo para Leitura.
            }


            // O Método "Opcao2()", é usado caso o utilizador pretenda ver os resultados Médios
            // de Probabilidade de chuva e Temperatura.
            //  - Após Comunicar com o respetivo url, irá obter o resultado da API dos Dados Gerais.
            //  - Vai iterar por todos os valores que pretende e efetuar os calculos necessários(Somas e Médias).
            //  - Vai organizar os dados e apresentar os mesmos ao utilizador.
            public async Task Opcao2(string url)
            {
                Funcoes f = new Funcoes();
                Console.Clear();
                f.Banner();
                HttpClient cliente = new HttpClient();
                var conteudo = await cliente.GetStringAsync(url); // Obter Dados Gerais.
                Modelo valores = JsonConvert.DeserializeObject<Modelo>(conteudo);

                int contador = 0; // Iterador
                float Tprecipit = 0; // Total de Probailidade de Chuva em Todo o Pais.
                float Ttemp = 0; // Total da Média das temperatura de Chuva em Todo o Pais.

                foreach (var i in valores.data)
                {
                    var precipit = i.precipitaProb;
                    var minima = i.tMin;
                    var maxima = i.tMax;

                    contador++;
                    Tprecipit += precipit;
                    Ttemp += ((minima + maxima) / 2);
                }

                Tprecipit = Tprecipit / (contador - 1);
                Ttemp = Ttemp / (contador - 1);

                // Apresentar dados Finais ao utilizador.
                Console.WriteLine("============================================");
                Console.WriteLine("Temperatura Média de todo o Pais: {0}º", String.Format("{0:0.00}", Tprecipit));
                Console.WriteLine("Probabilidade de Chuva em todo o Pais: {0}%", String.Format("{0:0.00}", Ttemp));
                Console.WriteLine("============================================");
                Console.ReadLine(); // Reter a conssola para dar tempo para Leitura.
            }

            // O Método "Opcao3()" é usado caso o utilizador pretenda escolher um local especifico,
            // de modo a obter todos os dados disponíveis sobre o mesmo.
            //  Este método segue uma lógica semelhante aos anteriores sendo que são usados todos os diferentes url's,
            //  de modo a podermos apresentar os dados todos perssonalizados, e em Portugues invês de ID's.
            public async Task Opcao3(string url, int localId)
            {
                Funcoes f = new Funcoes();
                Console.Clear();
                f.Banner();
                HttpClient cliente = new HttpClient();
                var conteudo = await cliente.GetStringAsync(url); // Obter Dados Gerais.
                Modelo valores = JsonConvert.DeserializeObject<Modelo>(conteudo);
                var conteudoVento = await cliente.GetStringAsync(urlVento); // Obter Dados Sobre os Ventos.
                ModeloVentos valoresVentos = JsonConvert.DeserializeObject<ModeloVentos>(conteudoVento);
                var conteudoTempo = await cliente.GetStringAsync(urlTempo); // Obter Dados sobre o Tipo de Tempo.
                ModeloTempo valoresTempo = JsonConvert.DeserializeObject<ModeloTempo>(conteudoTempo);
                var conteudoLocalidade = await cliente.GetStringAsync(urlLocalidades); // Obter Localidades.
                Localidades localidade = JsonConvert.DeserializeObject<Localidades>(conteudoLocalidade);

                // Alguns registos nem sempre devolvem informações.
                // De modo a reduzir problemas inesperados que comprometeriam o programa,
                // é usado este valor Bolleano para confirmar se existem ou não resultados disponiveis.
                bool result = false;

                string zona = ""; // Reter nome da Localidade que foi escolhida.s
                foreach (var i in localidade.data)
                {
                    if (localId == i.globalIdLocal)
                    {
                        zona = i.local;
                    }
                }

                foreach (var i in valores.data)
                {
                    if (i.globalIdLocal == localId)
                    {
                        result = true;
                        // Apresentação dos Resultados ao utilizador.
                        Console.WriteLine("====================={0}============================", zona);
                        Console.WriteLine("Probabilidade de Percipitação: {0}%", i.precipitaProb);
                        Console.WriteLine("Temperatura Minima: {0}º", i.tMin);
                        Console.WriteLine("Temperatura Máxima: {0}º", i.tMax);
                        Console.WriteLine("Direção do Vento: {0}", i.predWindDir);
                        foreach (var x in valoresTempo.data)
                        {
                            if (x.idWeatherType == i.idWeatherType)
                            {
                                Console.WriteLine("Tipo de Tempo: {0}", x.descIdWeatherTypePT);
                            }
                        }
                        foreach (var y in valoresVentos.data)
                        {
                            if (y.classWindSpeed == i.classWindSpeed)
                            {
                                Console.WriteLine("Velocidade de vento: {0}", y.descClassWindSpeedDailyPT);
                            }
                        }
                        Console.WriteLine("Longitude: {0}", i.longitude);
                        Console.WriteLine("Latitude: {0}", i.latitude);
                        Console.WriteLine("============================================================");
                    }
                }

                if (!result)
                {
                    Console.WriteLine("========================================================================");
                    Console.WriteLine("!! De momento não temos dados relativamente á localidade selecionada. !!");
                    Console.WriteLine("========================================================================");
                    Console.ReadLine(); // Reter a conssola para dar tempo para Leitura.
                }

                Console.ReadLine(); // Reter a conssola para dar tempo para Leitura.
            }

            // O Método "EscolhaDia()" é usado sempre que pretendemos questionar ao utilizador,
            // qual o dia que pretende saber os dados.
            public string EscolhaDia()
            {
                Funcoes f = new Funcoes();
                int resposta = 0;
                while (resposta == 0)
                {
                    Console.Clear();
                    f.Banner();
                    Console.WriteLine("Quer ver os resultados de que dia?");
                    Console.WriteLine("[1] - Hoje;");
                    Console.WriteLine("[2] - Amanhã;");
                    Console.WriteLine("[3] - Depois-de-Amanhã.");
                    Console.Write("## ");
                    string temp_resposta = Console.ReadLine();
                    if (f.OpcoesCheck(temp_resposta))
                    {
                        resposta = Convert.ToInt32(temp_resposta);
                        switch (resposta)
                        {
                            case 1:
                                return url1;
                            case 2:
                                return url2;
                            case 3:
                                return url3;
                            default:
                                Console.WriteLine("Não escolheu uma opção Válida!");
                                Console.ReadLine(); // Reter a conssola para dar tempo para Leitura.
                                resposta = 0;
                                Environment.Exit(0);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Não escolheu uma opção válida!");
                        Console.ReadLine(); // Reter a conssola para dar tempo para Leitura.
                        resposta = 0;
                    }
                }
                return "";
            }

            // O Método "EscolhaLocal()" é usado para auxciliar o método "Opcao3()", de modo a poder se saber,
            // o local que o utilizador quer usar para se saber os resultados.
            public int EscolhaLocal()
            {
                Funcoes f = new Funcoes();
                int resposta = 0;
                while (resposta == 0)
                {
                    Console.Clear();
                    f.Banner();
                    Console.WriteLine("Quer ver os resultados de que Localidade?");
                    Console.WriteLine("[1] - Aveiro \t\t\t\t [16] - Setúbal");
                    Console.WriteLine("[2] - Beja \t\t\t\t [17] - Viana do Castelo");
                    Console.WriteLine("[3] - Braga \t\t\t\t [18] - Vila Real");
                    Console.WriteLine("[4] - Bragança \t\t\t\t [19] - Viseu");
                    Console.WriteLine("[5] - Castelo Branco \t\t\t [20] - Funchal");
                    Console.WriteLine("[6] - Coimbra \t\t\t\t [21] - Porto Santo");
                    Console.WriteLine("[7] - Évora \t\t\t\t [22] - Vila do Porto");
                    Console.WriteLine("[8] - Faro \t\t\t\t [23] - Ponta Delgada");
                    Console.WriteLine("[9] - Guarda \t\t\t\t [24] - Agra do Heroísmo");
                    Console.WriteLine("[10] - Leiria \t\t\t\t [25] - Santa Cruz da Graciosa");
                    Console.WriteLine("[11] - Lisboa \t\t\t\t [26] - Velas");
                    Console.WriteLine("[12] - Lisboa - Jardim Botânico \t [27] -Madalena ");
                    Console.WriteLine("[13] - Portalegre \t\t\t [28] - Horta");
                    Console.WriteLine("[14] - Porto \t\t\t\t [29] - Santa Cruz das Flores");
                    Console.WriteLine("[15] - Santarém \t\t\t [30] - Vila do Corvo");
                    Console.Write("## ");
                    string temp_resposta = Console.ReadLine();
                    if (f.OpcoesCheck(temp_resposta))
                    {
                        resposta = Convert.ToInt32(temp_resposta);
                        switch (resposta)
                        {
                            case 1:
                                return 1010500;
                            case 2:
                                return 1010500;
                            case 3:
                                return 1030300;
                            case 4:
                                return 1040200;
                            case 5:
                                return 1050200;
                            case 6:
                                return 1060300;
                            case 7:
                                return 1070500;
                            case 8:
                                return 1080500;
                            case 9:
                                return 1090700;
                            case 10:
                                return 1090700;
                            case 11:
                                return 1110600;
                            case 12:
                                return 1110622;
                            case 13:
                                return 1121400;
                            case 14:
                                return 1131200;
                            case 15:
                                return 1141600;
                            case 16:
                                return 1151200;
                            case 17:
                                return 1160900;
                            case 18:
                                return 1171400;
                            case 19:
                                return 1182300;
                            case 20:
                                return 2310300;
                            case 21:
                                return 2320100;
                            case 22:
                                return 3410100;
                            case 23:
                                return 3420300;
                            case 24:
                                return 3430100;
                            case 25:
                                return 3440100;
                            case 26:
                                return 3450200;
                            case 27:
                                return 3460200;
                            case 28:
                                return 3470100;
                            case 29:
                                return 3480200;
                            case 30:
                                return 3490100;
                            default:
                                Console.WriteLine("Não escolheu uma opção Válida!");
                                Console.ReadLine(); // Reter a conssola para dar tempo para Leitura.
                                resposta = 0;
                                Environment.Exit(0);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Não escolheu uma opção válida!");
                        Console.ReadLine(); // Reter a conssola para dar tempo para Leitura.
                        resposta = 0;
                    }
                }
                return 0;
            }

        }


        public static void Main(string[] args)
        {
            //Instanciamento de class Funcoes.
            //  Nesta class vamos ter acesso a todos os métodos que foram criados para executar certas ações.
            Funcoes f = new Funcoes();

            int opc = 0;
            string url;
            int localId;

            // Loop While "Infinito"
            //  Aqui forçamos a conssola num ciclo constante. Deste modo o utilizador pode exeutar os comandos,
            //  as vezes que quiser, dando assim uma continuidade á utilização do programa.
            while (opc == 0)
            {
                Console.Clear();
                f.Banner();
                Console.WriteLine("O que Pretende ver? (Escolha uma Opção)");
                Console.WriteLine("[1] - Ver Listagem de Temperaturas;");
                Console.WriteLine("[2] - Ver a Média das Temperaturas do Pais;");
                Console.WriteLine("[3] - Escolher Região Especifica.");
                Console.Write("## ");
                string temp_opc = Console.ReadLine();
                if (f.OpcoesCheck(temp_opc))
                {
                    opc = Convert.ToInt32(temp_opc);
                    switch (opc)
                    {
                        case 1:
                            url = f.EscolhaDia();
                            f.Opcao1(url).Wait();
                            break;
                        case 2:
                            url = f.EscolhaDia();
                            f.Opcao2(url).Wait();
                            break;
                        case 3:
                            url = f.EscolhaDia();
                            localId = f.EscolhaLocal();
                            f.Opcao3(url, localId).Wait();
                            break;
                        default:
                            Console.WriteLine("Não escolheu uma opção Válida!");
                            opc = 0;
                            Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Não escolheu uma opção Válida!");
                    Console.ReadLine();
                }

                // Saida do While Loop:
                //  É aqui questionado ao utilizador se este pretende sair ou não do programa.
                //  Terminando o Loop e consequentemente terminando o programa.
                Console.Clear();
                f.Banner();
                Console.WriteLine("\t\t\t\t\tDeseja continuar? (S)im ou (N)ao");
                Console.Write("## ");
                string exit = Console.ReadLine();
                if (exit == "s" || exit == "S" || exit == "sim" || exit == "Sim")
                {
                    opc = 0;
                }
                else
                {
                    Console.Clear();
                    f.Banner();
                    Console.WriteLine("\t\t\t\t\tObrigado pela sua utilização. Adeus!");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
            Console.ReadLine(); // Reter a conssola para dar tempo para Leitura.
        }
    }
}
