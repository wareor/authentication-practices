using CatFactsAPI.Models;
using System.Net.Http;
using System.Text.Json;

namespace CatFactsAPI.Services
{
    public class CatFactExternalAPI
    {
        private string _urlBase;
        public CatFactExternalAPI(string urlBase)
        {
            //_urlBase = "https://catfact.ninja/";
            _urlBase = urlBase;
        }

        public async Task<List<CatFact>> Get()
        {

            HttpClient httpClient = new()
            {
                BaseAddress = new Uri(_urlBase),
            };

            using HttpResponseMessage response = await httpClient.GetAsync("fact");

            var ensureResponseSuccessSatusCdode = response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            CatFact? catFacts = JsonSerializer.Deserialize<CatFact>(jsonResponse);
            var cfL = new List<CatFact>
            {
                catFacts
            };
            return cfL;
        }
    }
}
