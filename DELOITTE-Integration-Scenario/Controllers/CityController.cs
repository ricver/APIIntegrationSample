using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DELOITTE_Integration_Scenario.Models;

namespace DELOITTE_Integration_Scenario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly CityDBContext _context;

        public CityController(CityDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<City>> Get()
        {
            return await _context.Cities.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetbyID(int id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
                return NotFound();
            return Ok(city);

        }
        [HttpGet("{name}")]
        public async Task<ActionResult<City>> GetbyName(string name)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(m => m.Name == name);
            if (city == null)
                return NotFound();
            return Ok(city);

        }

        [HttpPost]
        public async Task<ActionResult> Post(City city)
        {
            try
            {
                await _context.AddAsync(city);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(City cityData)
        {
            if (cityData == null || cityData.Id == 0)
                return BadRequest();

            var city = await _context.Cities.FindAsync(cityData.Id);
            if (city == null)
                return NotFound();
            city.Name = cityData.Name;
            city.State = cityData.State;
            city.Country = cityData.Country;
            city.Rating = cityData.Rating;
            city.EstimatedPopulation = cityData.EstimatedPopulation;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _context.Cities.FindAsync(id);
            if (product == null) return NotFound();
            _context.Cities.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
