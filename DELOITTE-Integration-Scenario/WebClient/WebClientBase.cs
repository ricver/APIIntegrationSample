using DELOITTE_Integration_Scenario.Models;

namespace DELOITTE_Integration_Scenario.WebClient
{
    public class WebClientBase
    {
        protected HttpClient client = new HttpClient();
        protected string CountriesAPIBaseURL = "https://restcountries.com/v2/name/";
        protected string CityWeatherAPIBaseURL = "https://api.openweathermap.org/data/2.5/weather?q=";
        protected string CityWeatherAPIKEY = ",uk&APPID=5a18fdfc95e16687a4f0dc19c825add1";
        protected async Task<string> SendAPIRequest(string BaseURL, string param)
        {
            string url = BaseURL+param;
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
