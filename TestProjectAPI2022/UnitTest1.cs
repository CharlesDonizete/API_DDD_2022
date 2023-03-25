using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace TestProjectAPI2022
{
    [TestClass]
    public class UnitTest1
    {

        public static string Token { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            var result = ChamaApiPost("https://localhost:7212/api/List").Result;
            var listaMessagem = JsonConvert.DeserializeObject<Message[]>(result)?.ToList();

            Assert.IsTrue(listaMessagem.Any());
        }

        public void GetToken()
        {
            string urlApiGeraToken = "https://localhost:7212/api/CriarTokenIdentity";

            using (var cliente = new HttpClient())
            {
                string login = "charles@gmail.com";
                string senha = "@123QWEqwe";
                var dados = new
                {
                    email = login,
                    senha = senha,
                    cpf = "2121321313"
                };
                string JsonObjeto = JsonConvert.SerializeObject(dados);
                var content = new StringContent(JsonObjeto, Encoding.UTF8, "application/json");

                var resultado = cliente.PostAsync(urlApiGeraToken, content);
                resultado.Wait();

                if (resultado.Result.IsSuccessStatusCode)
                {
                    var tokenJson = resultado.Result.Content.ReadAsStringAsync();
                    Token = JsonConvert.DeserializeObject(tokenJson.Result).ToString();
                }
            }
        }

        public string ChamaApiGet(string url)
        {
            GetToken();
            if (!string.IsNullOrWhiteSpace(Token))
            {
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Clear();
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    var response = cliente.GetStringAsync(url);
                    response.Wait();
                    return response.Result;
                }
            }

            return null;
        }

        public async Task<string> ChamaApiPost(string url, object dados = null)
        {

            string JsonObjeto = dados != null ? JsonConvert.SerializeObject(dados) : "";
            var content = new StringContent(JsonObjeto, Encoding.UTF8, "application/json");

            GetToken(); // Gerar token
            if (!string.IsNullOrWhiteSpace(Token))
            {
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Clear();
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    var response = cliente.PostAsync(url, content);
                    response.Wait();
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var retorno = await response.Result.Content.ReadAsStringAsync();

                        return retorno;
                    }
                }
            }

            return null;

        }
    }
}