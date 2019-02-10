Este trabalho surge a pedido do Professor Sandro Ferreira no módulo de Programação Orientada a Objetos, do [Instituto Superior de Tecnologias Avançadas](http://www.istec.pt/).


Com este trabalho procurava-se ser documentado e criado um programa capaz de comunicar com a API do Instituo Português da Meteorologia e Atmosfera de modo a poder retornar ao utilizador final uma previsão da meteorologia pendente o dia e o local escolhidos. Para este programa ser aprovado este teria de cumprir os seguintes requisitos: 

- Apresente uma lista de temperaturas mínimas e máximas para todas as regiões disponibilizadas na API;

- Calcule a temperatura e precipitação média para todo o país;

- Permita especificar uma região e a data, obtendo todos os resultados para a data especificada.

Com estes requisitos em mente, neste trabalho será documentado o meu processo lógico em torno da aplicação construida. Note-se que decidi desde partida construir a mesma através da FrameWork “Console Apps”, ou seja, será um programa sem qualquer interface gráfico, sendo que todo o seu manuseamento será efetuado com recurso a um terminal.

De destacar também que esta aplicação foi construida num sistema de base Windows10, onde todo o código foi escrito no [Visual Studio Community](https://visualstudio.microsoft.com/pt-br/vs/), e o programa foi compilado e executado com ajuda dos utilitários provenientes do mesmo. Note-se ainda que foram adicionadas as FrameWorks “[Newtonsoft Json.NET](https://www.newtonsoft.com/json)” para manipulação de Json’s e “[System.Net.Http](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netframework-4.7.2)”para efetuar os pedidos à API.
