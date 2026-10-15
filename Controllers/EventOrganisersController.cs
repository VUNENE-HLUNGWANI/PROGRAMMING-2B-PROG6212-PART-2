using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;
using RaceDayAPI.Filters;

namespace RaceDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SessionAuthorize("Organiser")]
    public class EventOrganisersController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public EventOrganisersController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/EventOrganisers
        [HttpGet]
        public async Task<IActionResult> GetEventOrganisers()
        {
            var organisers = await _context.EventOrganisers
                .Include(eo => eo.User)
                .ToListAsync();
            return Ok(organisers);
        }

        // GET: api/EventOrganisers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventOrganiser(int id)
        {
            var eventOrganiser = await _context.EventOrganisers
                .Include(eo => eo.User)
                .FirstOrDefaultAsync(eo => eo.EventOrganiserID == id);

            if (eventOrganiser == null)
            {
                return NotFound(new { message = $"EventOrganiser with ID {id} not found." });
            }

            return Ok(eventOrganiser);
        }

        // PUT: api/EventOrganisers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventOrganiser(int id, [FromBody] EventOrganiser eventOrganiser)
        {
            if (id != eventOrganiser.EventOrganiserID)
            {
                return BadRequest(new { message = "ID mismatch between route and payload." });
            }

            _context.Entry(eventOrganiser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventOrganiserExists(id))
                {
                    return NotFound(new { message = $"EventOrganiser with ID {id} not found." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/EventOrganisers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventOrganiser(int id)
        {
            var eventOrganiser = await _context.EventOrganisers.FindAsync(id);
            if (eventOrganiser == null)
            {
                return NotFound(new { message = $"EventOrganiser with ID {id} not found." });
            }

            _context.EventOrganisers.Remove(eventOrganiser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventOrganiserExists(int id)
        {
            return _context.EventOrganisers.Any(e => e.EventOrganiserID == id);
        }
    }
}