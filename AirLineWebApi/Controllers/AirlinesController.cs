using AirlineDll;
using AirlineDll.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AirLineWebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AirlinesController : ControllerBase
    {
        private readonly AirlineDbContext _context;

        public AirlinesController(AirlineDbContext context)
        {
            _context = context;
        }
        // GET: api/Airlines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Airline>>> GetAirlines()
        {
            if (_context.Airlines == null)
            {
                return NotFound();
            }
            else
            {
                var list = await _context.Airlines.ToListAsync();
               var json = JsonConvert.SerializeObject(list);
                return Ok(json);
            }
        }
        // GET: api/Airlines/5
        [HttpGet("Id")]
        public async Task<ActionResult<Airline>> GetAirLineById(int id)
        {
          if (_context.Airlines == null)
          {
              return NotFound();
          }
            var data = await _context.Airlines.FirstOrDefaultAsync(a => a.Id == id);
            var json = JsonConvert.SerializeObject(data);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(json);
        }

        [HttpGet("Name")]
        public async Task<ActionResult<Airline>> GetAirlineByName(string Name)
        {
            if (_context.Airlines == null)
            {
                return NotFound();
            }
            var data = await _context.Airlines.FirstOrDefaultAsync(a => a.Name == Name);
            var json = JsonConvert.SerializeObject(data);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(json);
        }


        // PUT: api/Airlines/5
        [HttpPut]
        public async Task<IActionResult> UpdateAirLines(Airline airline)
        {
            if (_context.Airlines == null)
            {
                return NotFound();
            }
            _context.Airlines.Update(airline);
            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
        // POST: api/Airlines
        [HttpPost]
        public async Task<ActionResult<Airline>> CreateAirLines(Airline airlineModel)
        {
          if (_context.Airlines == null)
          {
              return Problem("Entity set 'AirlineDbContext.Airlines'  is null.");
          }
            if (airlineModel != null)
            {
                _context.Airlines.Add(airlineModel);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
                return BadRequest();
        }
        // DELETE: api/Airlines/5
        [HttpDelete]
        public async Task<IActionResult> DeleteAirlines(int id)
        {
            if (_context.Airlines == null)
            {
                return NotFound();
            }
            var airline = await _context.Airlines.FirstOrDefaultAsync(a => a.Id == id);
            if (airline == null)
            {
                return NotFound();
            }
            _context.Airlines.Remove(airline);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
