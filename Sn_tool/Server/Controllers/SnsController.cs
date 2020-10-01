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
    public class SnsController : ControllerBase
    {
        private readonly TMSContext _context;

        public SnsController(TMSContext context)
        {
            _context = context;
        }

        // GET: api/Sns1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sn>>> GetSn()
        {
            return await _context.Sn_TV.ToListAsync();
        }

        // GET: api/Sns1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sn>> GetSn(int id)
        {
            var sn = await _context.Sn_TV.FindAsync(id);

            if (sn == null)
            {
                return NotFound();
            }
 
            return sn;
        }

        // ------------------------Wyszukiwanie po numerze seryjnym 
        // GET: Get Sn ny Sn 
          [HttpGet("GetSnBySn/{snNR}")]
       // [HttpGet(nameof(GetSnBySn))]
        public async Task<ActionResult<IEnumerable<Sn>>> GetSnBySn(string snNR)
        {
           
            var temp= await _context.Sn_TV.Where(s => s.SerialNumber.Trim() == snNR.Trim()).ToListAsync();
           
          
            return temp;
        }
        // ------------------------Wyszukiwanie po ship to name  
        [HttpGet("GetSnByShipTo/{snShipToName}")]
        public async Task<ActionResult<IEnumerable<Sn>>> GetSnByShipToName(string snShipToName)
        {
            return await _context.Sn_TV.Where(s => s.ShiptoName.Trim() == snShipToName.Trim()).ToListAsync();
        }

        // ------------------------Wyszukiwanie po ship to number 0 ID
        [HttpGet("GetSnByShipToNr/{snShipToNumber}")]
        public async Task<ActionResult<IEnumerable<Sn>>> GetSnByShipToNr(string snShipToNumber)
        {
            return await _context.Sn_TV.Where(s => s.ShiptoNumber.Trim() == snShipToNumber.Trim()).ToListAsync();
        }


        // ------------------------Wyszukiwanie po ship to SAP delivery number
        [HttpGet("GetSnByDN/{snDn}")]
        public async Task<ActionResult<IEnumerable<Sn>>> GetSnByDN(string snDn)
        {
            return await _context.Sn_TV.Where(s => s.SapdeliveryNumber.Trim() == snDn.Trim()).ToListAsync();
        }


        // PUT: api/Sns1/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSn(int id, Sn sn)
        {
            if (id != sn.Id)
            {
                return BadRequest();
            }

            _context.Entry(sn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SnExists(id))
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

        // POST: api/Sns1
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Sn>> PostSn(Sn sn)
        {
            _context.Sn_TV.Add(sn);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSn", new { id = sn.Id }, sn);
        }

        // DELETE: api/Sns1/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sn>> DeleteSn(int id)
        {
            var sn = await _context.Sn_TV.FindAsync(id);
            if (sn == null)
            {
                return NotFound();
            }

            _context.Sn_TV.Remove(sn);
            await _context.SaveChangesAsync();

            return sn;
        }

        private bool SnExists(int id)
        {
            return _context.Sn_TV.Any(e => e.Id == id);
        }
    }
}
