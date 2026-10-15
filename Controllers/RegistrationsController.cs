using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [Route("api/enrolments")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public RegistrationsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/enrolments
        [HttpGet]
        public async Task<IActionResult> GetRegistrations()
        {
            var registrations = await _context.ParticipantEvents
                .Include(pe => pe.Participant)
                .Include(pe => pe.Event)
                .Include(pe => pe.Category)
                .ToListAsync();

            return Ok(registrations);
        }

        // GET: api/enrolments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistration(int id)
        {
            var registration = await _context.ParticipantEvents
                .Include(pe => pe.Participant)
                .Include(pe => pe.Event)
                .Include(pe => pe.Category)
                .FirstOrDefaultAsync(pe => pe.ParticipantEventID == id);

            if (registration == null)
            {
                return NotFound(new { message = "Registration not found." });
            }

            return Ok(registration);
        }

        // GET: api/events/{eventId}/enrolments 
        [HttpGet("/api/events/{eventId}/enrolments")]
        public async Task<IActionResult> GetRegistrationsByEvent(int eventId)
        {
            var registrations = await _context.ParticipantEvents
                .Include(pe => pe.Participant)
                .Include(pe => pe.Category)
                .Where(pe => pe.EventID == eventId)
                .ToListAsync();

            return Ok(registrations);
        }

        // POST: api/enrolments
        [HttpPost]
        public async Task<IActionResult> CreateRegistration([FromBody] ParticipantEvent registrationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var participant = await _context.Participants.FindAsync(registrationDto.ParticipantID);
                if (participant == null)
                {
                    return BadRequest(new { message = "Invalid ParticipantID. Participant does not exist." });
                }

                var raceEvent = await _context.Events.FindAsync(registrationDto.EventID);
                if (raceEvent == null)
                {
                    return BadRequest(new { message = "Invalid EventID. Event does not exist." });
                }

                // Validate registration deadline
                if (DateTime.Now > raceEvent.RegistrationDeadline)
                {
                    return BadRequest(new { message = "Registration deadline for this event has passed." });
                }

                var category = await _context.Categories.FindAsync(registrationDto.CategoryID);
                if (category == null)
                {
                    return BadRequest(new { message = "Invalid CategoryID. Category does not exist." });
                }

                if (category.EventID != registrationDto.EventID)
                {
                    return BadRequest(new { message = "Category does not belong to this event." });
                }

                var alreadyRegistered = await _context.ParticipantEvents.AnyAsync(pe =>
                    pe.ParticipantID == registrationDto.ParticipantID &&
                    pe.EventID == registrationDto.EventID);

                if (alreadyRegistered)
                {
                    return Conflict(new { message = "Participant is already registered for this event." });
                }

                registrationDto.RegistrationDate = DateTime.Now;
                registrationDto.RegistrationStatus = "Pending"; // Default status set to Pending 

                _context.ParticipantEvents.Add(registrationDto);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetRegistration), new { id = registrationDto.ParticipantEventID }, new { message = "Registration created successfully", participantEventId = registrationDto.ParticipantEventID });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error." });
            }
        }
    }
}