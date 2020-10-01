using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sn_tool.Server.Data;
using Sn_tool.Shared;

namespace Sn_tool.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VTVDailyquatitySnController : ControllerBase
    {
        private readonly TMSContext _context;

        public VTVDailyquatitySnController(TMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VTVDailyquatitySn>>> GetImportLog()
        {
            return await _context.VTVDailyquatitySn.OrderBy(c => c.DateScanned).ToListAsync();
        }
    }
}
