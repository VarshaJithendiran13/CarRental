using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;

namespace CarRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordResetsController : ControllerBase
    {
        private readonly YourDbContext _context;

        public PasswordResetsController(YourDbContext context)
        {
            _context = context;
        }

        // GET: api/PasswordResets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PasswordReset>>> GetPasswordResets()
        {
            return await _context.PasswordResets.ToListAsync();
        }

        // GET: api/PasswordResets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PasswordReset>> GetPasswordReset(int id)
        {
            var passwordReset = await _context.PasswordResets.FindAsync(id);

            if (passwordReset == null)
            {
                return NotFound();
            }

            return passwordReset;
        }

        // PUT: api/PasswordResets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPasswordReset(int id, PasswordReset passwordReset)
        {
            if (id != passwordReset.ResetId)
            {
                return BadRequest();
            }

            _context.Entry(passwordReset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasswordResetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PasswordResets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PasswordReset>> PostPasswordReset(PasswordReset passwordReset)
        {
            _context.PasswordResets.Add(passwordReset);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPasswordReset", new { id = passwordReset.ResetId }, passwordReset);
        }

        // DELETE: api/PasswordResets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePasswordReset(int id)
        {
            var passwordReset = await _context.PasswordResets.FindAsync(id);
            if (passwordReset == null)
            {
                return NotFound();
            }

            _context.PasswordResets.Remove(passwordReset);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PasswordResetExists(int id)
        {
            return _context.PasswordResets.Any(e => e.ResetId == id);
        }
    }
}
