using DELOITTE_Integration_Scenario.Controllers;
using DELOITTE_Integration_Scenario.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Shouldly;
using System.Collections.Generic;
using System.Net;
using DELOITTE_Integration_Scenario.WebClient;

namespace WebAPITest
{
    public class CitiesControllerTest
    {
        private readonly CityController _controller;
        private readonly CityDBContext _context;
        public CitiesControllerTest()
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = config.GetConnectionString("CityDB");
            var builder = new DbContextOptionsBuilder<CityDBContext>();
            builder.UseSqlServer(connectionString);

            _context = new CityDBContext(builder.Options);
            _controller = new CityController(_context);
        }


        [Fact]
        public async Task GETCountry()
        {

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://restcountries.com/v2/name/" + "Peru");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            List<CountryByName> CounntryDeserialised = JsonConvert.DeserializeObject<List<CountryByName>>(responseBody);
            
            Assert.Equal("Peru", (CounntryDeserialised[0].name));
        }

        [Fact]
        public async void AddNewCity() //Add City
        {
            // Arrange
            City city = new City();
            city.Name = "London";
            city.State = "";
            city.Country = "England";
            city.Rating = 5;
            city.EstimatedPopulation = 50000;

            // Act
            await _controller.Post(city);
            // Assert

        }

        [Fact]
        public async void UpdateCity() //Update City
        {
            // Arrange
            City city = new City();
            city.Name = "London";
            city.State = "";
            city.Country = "United Kingdom";
            city.Rating = 5;
            city.EstimatedPopulation = 50000;

            // Act
            await _controller.Put(city);
            // Assert

        }

        [Fact]
        public async void DeleteCity() //Delete City
        {
            // Arrange
            var response = await _controller.Get();

            // Act
            await _controller.Delete(response[0].Id);
            // Assert
        }
        [Fact]
        public async void SearchCity()
        {
            string CityName = "London";
            //get city
            ActionResult<City> response = await _controller.GetbyName(CityName);
            //response.Result
            var res = (OkObjectResult)response.Result;
            if (res.Value == null)
                return;

            var country = ((City)res.Value).Country;
            CountriesAPI CAPI = new CountriesAPI();
            Task<List<CountryByName>> countryByName = CAPI.GetCountryAsync(country);
            //2 digit country
            var Twocode = countryByName.Result[0].alpha2Code;
            //3 digit country code
            var Threecode = countryByName.Result[0].alpha3Code;
            //currency code
            var cCode = countryByName.Result[0].currencies[0].code;
            //weather for the city.
            //return country --> get country and currency codes
            OpenWeatherMapAPI WeatherByCity = new OpenWeatherMapAPI();
            Task<WeatherByCity> weatherByCity = WeatherByCity.GetCityWeatherAsync(CityName);
            string WeatherDesc;
            //Get weather
            //if (weatherByCity.Result.Count == 0)
            //    return;
            //else if (weatherByCity.Result.Count == 1)
            //   WeatherDesc = weatherByCity.Result[0].weather[0].description;
            //else
            //    return; //return all items 
        }
    }
}