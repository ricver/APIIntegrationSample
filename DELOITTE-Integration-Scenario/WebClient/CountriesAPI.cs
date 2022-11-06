using DELOITTE_Integration_Scenario.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DELOITTE_Integration_Scenario.WebClient
{
    public class CountriesAPI : WebClientBase
    {
        public async Task<List<CountryByName>> GetCountryAsync(string Country)
        {
            var responseBody = SendAPIRequest(CountriesAPIBaseURL, Country); //await response.Content.ReadAsStringAsync();
            List<CountryByName> CountryDeserialised = JsonConvert.DeserializeObject<List<CountryByName>>(responseBody.Result);

            return CountryDeserialised;
        }
    }
}
