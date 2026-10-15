using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public ParticipantsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/participants
        [HttpGet]
        public async Task<IActionResult> GetParticipants()
        {
            var participants = await _context.Participants
                .Include(p => p.User)
                .ToListAsync();
            return Ok(participants);
        }

        // POST: api/participants
        [HttpPost]
        public async Task<IActionResult> CreateParticipant([FromBody] Participant participantDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // If the incoming DTO has a User object nested, check/save it or attach it
                if (participantDto.User != null)
                {
                    var existingUser = await _context.Users.FindAsync(participantDto.User.UserID);
                    if (existingUser != null)
                    {
                        participantDto.User = existingUser;
                        participantDto.ParticipantID = existingUser.UserID;
                    }
                    else
                    {
                        // Add the new user first if it doesn't exist
                        _context.Users.Add(participantDto.User);
                        await _context.SaveChangesAsync();
                        participantDto.ParticipantID = participantDto.User.UserID;
                    }
                }

                _context.Participants.Add(participantDto);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetParticipants), new { id = participantDto.ParticipantID }, new { message = "Participant created successfully", participantID = participantDto.ParticipantID });
            }
            catch (Exception)
            {
                // Removed exception and inner exception exposure
                return StatusCode(500, new { message = "Internal server error." });
            }
        }
    }
}