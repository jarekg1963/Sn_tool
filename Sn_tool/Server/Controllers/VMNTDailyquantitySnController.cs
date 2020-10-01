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
    public class VMNTDailyquantitySnController : ControllerBase
    {
        private readonly TMSContext _context;

        public VMNTDailyquantitySnController(TMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VMNTDailyquantitySn>>> GetImportLog()
        {
            return await _context.VMNTDailyquantitySn.OrderBy(c => c.GoodsIssueDate).ToListAsync();
        }
    }
}
