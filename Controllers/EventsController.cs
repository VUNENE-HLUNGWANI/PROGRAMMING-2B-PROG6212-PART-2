using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public EventsController(RaceDayDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all scheduled racing events.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific event by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var raceEvent = await _context.Events.FindAsync(id);

            if (raceEvent == null)
                return NotFound(new { message = "Event not found." });

            return raceEvent;
        }

        /// <summary>
        /// Creates a new event. Restricted to Organiser roles via Session verification.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] Event raceEvent)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Organiser")
            {
                return StatusCode(403, new { message = "Access denied. Organiser role required." });
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Events.Add(raceEvent);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvent), new { id = raceEvent.EventID }, raceEvent);

        }

        /// <summary>
        /// Deletes an event. Restricted to Organiser roles.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Organiser")
            {
                return StatusCode(403, new { message = "Access denied. Organiser role required." });
            }

            var raceEvent = await _context.Events.FindAsync(id);
            if (raceEvent == null)
                return NotFound(new { message = "Event not found." });

            _context.Events.Remove(raceEvent);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Event deleted successfully." });
        }
    }
}