using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sn_tool.Server.Data;
using Sn_tool.Shared.Data;

namespace Sn_tool.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportLogsController : ControllerBase
    {
        private readonly TMSContext _context;

        public ImportLogsController(TMSContext context)
        {
            _context = context;
        }

        // GET: api/ImportLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImportLog>>> GetImportLog()
        {
            return await _context.ImportLog.OrderByDescending(i => i.DataLog).ToListAsync();
        }

        // GET: api/ImportLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImportLog>> GetImportLog(int id)
        {
            var importLog = await _context.ImportLog.FindAsync(id);

            if (importLog == null)
            {
                return NotFound();
            }

            return importLog;
        }

        // PUT: api/ImportLogs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImportLog(int id, ImportLog importLog)
        {
            if (id != importLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(importLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImportLogExists(id))
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

        // POST: api/ImportLogs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ImportLog>> PostImportLog(ImportLog importLog)
        {
            _context.ImportLog.Add(importLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImportLog", new { id = importLog.Id }, importLog);
        }

        // DELETE: api/ImportLogs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ImportLog>> DeleteImportLog(int id)
        {
            var importLog = await _context.ImportLog.FindAsync(id);
            if (importLog == null)
            {
                return NotFound();
            }

            _context.ImportLog.Remove(importLog);
            await _context.SaveChangesAsync();

            return importLog;
        }

        private bool ImportLogExists(int id)
        {
            return _context.ImportLog.Any(e => e.Id == id);
        }
    }
}
