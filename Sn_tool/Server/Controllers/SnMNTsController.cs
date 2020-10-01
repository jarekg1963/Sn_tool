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
    public class SnMNTsController : ControllerBase
    {
        private readonly TMSContext _context;

        public SnMNTsController(TMSContext context)
        {
            _context = context;
        }

        // GET: api/SnMNTs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SnMNT>>> GetSnMNT()
        {
            return await _context.Sn_MNT.ToListAsync();
        }

        // GET: api/SnMNTs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SnMNT>> GetSnMNT(int id)
        {
            var snMNT = await _context.Sn_MNT.FindAsync(id);

            if (snMNT == null)
            {
                return NotFound();
            }

            return snMNT;
        }


        // ------------------------Wyszukiwanie po numerze seryjnym 
        // GET: Get Sn ny Sn 
        [HttpGet("GetSnByMNTSn/{snMNTNR}")]
        public async Task<ActionResult<IEnumerable<SnMNT>>> GetSnByMNTSn(string snMNTNR)
        {
            return await _context.Sn_MNT.Where(s => s.SerialNumber.Trim() == snMNTNR.Trim()).ToListAsync();
        }


        // GET: DN
        [HttpGet("GetSnByMNTDn/{MNTDn}")]
        public async Task<ActionResult<IEnumerable<SnMNT>>> GetSnByMNTDn(string MNTDn)
        {
            return await _context.Sn_MNT.Where(s => s.DN.Trim() == MNTDn.Trim()).ToListAsync();
        }

        // GET: DN
        // [HttpGet("GetSnByMNTPn/{MNTPn}")]
        [HttpGet(nameof(GetSnByMNTPn))]
        public async Task<ActionResult<IEnumerable<SnMNT>>> GetSnByMNTPn(string MNTPn)
        {
            return await _context.Sn_MNT.Where(s => s.PartNumber.Trim() == MNTPn.Trim()).ToListAsync();
        }

        [HttpGet("GetSnByMNTStn/{MNTStn}")]
        public async Task<ActionResult<IEnumerable<SnMNT>>> GetSnByMNTStn(string MNTStn)
        {
            return await _context.Sn_MNT.Where(s => s.ShipToName.Trim() == MNTStn.Trim()).ToListAsync();
        }


        // PUT: api/SnMNTs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSnMNT(int id, SnMNT snMNT)
        {
            if (id != snMNT.ID)
            {
                return BadRequest();
            }

            _context.Entry(snMNT).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SnMNTExists(id))
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

        // POST: api/SnMNTs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SnMNT>> PostSnMNT(SnMNT snMNT)
        {
            _context.Sn_MNT.Add(snMNT);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSnMNT", new { id = snMNT.ID }, snMNT);
        }

        // DELETE: api/SnMNTs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SnMNT>> DeleteSnMNT(int id)
        {
            var snMNT = await _context.Sn_MNT.FindAsync(id);
            if (snMNT == null)
            {
                return NotFound();
            }

            _context.Sn_MNT.Remove(snMNT);
            await _context.SaveChangesAsync();

            return snMNT;
        }

        private bool SnMNTExists(int id)
        {
            return _context.Sn_MNT.Any(e => e.ID == id);
        }
    }
}
