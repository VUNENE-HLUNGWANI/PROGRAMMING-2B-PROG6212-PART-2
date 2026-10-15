using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;
using RaceDayAPI.Filters;

namespace RaceDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public ResultsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResults()
        {
            return await _context.Results
                .Include(r => r.ParticipantEvent)
                .ToListAsync();
        }

        // GET: api/results/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(int id)
        {
            var result = await _context.Results
                .Include(r => r.ParticipantEvent)
                .FirstOrDefaultAsync(r => r.ResultID == id);

            if (result == null)
            {
                return NotFound(new { message = $"Result with ID {id} not found." });
            }

            return result;
        }

        // GET: api/results/participant-event/1
        [HttpGet("participant-event/{participantEventId}")]
        public async Task<ActionResult<IEnumerable<Result>>> GetResultsByParticipantEvent(int participantEventId)
        {
            var results = await _context.Results
                .Include(r => r.ParticipantEvent)
                .Where(r => r.ParticipantEventID == participantEventId)
                .ToListAsync();

            if (results == null || results.Count == 0)
            {
                return NotFound(new { message = $"No results found for ParticipantEvent ID {participantEventId}." });
            }

            return Ok(results);
        }

        // POST: api/results (Organiser Only)
        [HttpPost]
        [SessionAuthorize("Organiser")]
        public async Task<ActionResult<Result>> PostResult(Result result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResult), new { id = result.ResultID }, result);
        }

        // PUT: api/results/5 (Organiser Only)
        [HttpPut("{id}")]
        [SessionAuthorize("Organiser")]
        public async Task<IActionResult> PutResult(int id, Result result)
        {
            if (id != result.ResultID)
            {
                return BadRequest(new { message = "ID mismatch between route and payload." });
            }

            _context.Entry(result).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(id))
                {
                    return NotFound(new { message = $"Result with ID {id} not found." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/results/5 (Organiser Only)
        [HttpDelete("{id}")]
        [SessionAuthorize("Organiser")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound(new { message = $"Result with ID {id} not found." });
            }

            _context.Results.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResultExists(int id)
        {
            return _context.Results.Any(e => e.ResultID == id);
        }
    }
}