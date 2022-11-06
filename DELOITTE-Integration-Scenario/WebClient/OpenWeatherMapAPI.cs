using DELOITTE_Integration_Scenario.Models;
using Newtonsoft.Json;

namespace DELOITTE_Integration_Scenario.WebClient
{
    public class OpenWeatherMapAPI: WebClientBase
    {
        public async Task<WeatherByCity> GetCityWeatherAsync(string city)
        {
            var responseBody = SendAPIRequest(CityWeatherAPIBaseURL, city+ CityWeatherAPIKEY); //await response.Content.ReadAsStringAsync();
            WeatherByCity WeatherByCityDeserialised = JsonConvert.DeserializeObject<WeatherByCity>(responseBody.Result);
            //This needs to be made such that it can handle multiple countries returned
            return WeatherByCityDeserialised;
        }
    }
}
