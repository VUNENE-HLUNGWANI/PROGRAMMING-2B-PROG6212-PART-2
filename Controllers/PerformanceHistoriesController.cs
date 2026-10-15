using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;
using RaceDayAPI.Filters;

namespace RaceDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceHistoriesController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public PerformanceHistoriesController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/PerformanceHistories (Public view)
        [HttpGet]
        public async Task<IActionResult> GetPerformanceHistories()
        {
            var histories = await _context.PerformanceHistories
                .Include(ph => ph.Participant)
                .ToListAsync();
            return Ok(histories);
        }

        // GET: api/PerformanceHistories/5 (Public view)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerformanceHistory(int id)
        {
            var history = await _context.PerformanceHistories
                .Include(ph => ph.Participant)
                .FirstOrDefaultAsync(ph => ph.PerformanceHistoryID == id);

            if (history == null)
            {
                return NotFound(new { message = "Performance history not found." });
            }

            return Ok(history);
        }

        // POST: api/PerformanceHistories (Organiser Only)
        [HttpPost]
        [SessionAuthorize("Organiser")]
        public async Task<IActionResult> PostPerformanceHistory([FromBody] PerformanceHistory performanceHistory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.PerformanceHistories.Add(performanceHistory);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPerformanceHistory), new { id = performanceHistory.PerformanceHistoryID }, performanceHistory);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error." });
            }
        }

        // PUT: api/PerformanceHistories/5 (Organiser Only)
        [HttpPut("{id}")]
        [SessionAuthorize("Organiser")]
        public async Task<IActionResult> PutPerformanceHistory(int id, [FromBody] PerformanceHistory performanceHistory)
        {
            if (id != performanceHistory.PerformanceHistoryID)
            {
                return BadRequest(new { message = "ID mismatch." });
            }

            _context.Entry(performanceHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PerformanceHistories.Any(e => e.PerformanceHistoryID == id))
                {
                    return NotFound(new { message = "Performance history not found." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/PerformanceHistories/5 (Organiser Only)
        [HttpDelete("{id}")]
        [SessionAuthorize("Organiser")]
        public async Task<IActionResult> DeletePerformanceHistory(int id)
        {
            var history = await _context.PerformanceHistories.FindAsync(id);
            if (history == null)
            {
                return NotFound(new { message = "Performance history not found." });
            }

            _context.PerformanceHistories.Remove(history);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}