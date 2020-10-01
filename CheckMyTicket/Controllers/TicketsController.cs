using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CheckMyTicket.Models;

namespace CheckMyTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketContext _context;

        public TicketsController(TicketContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _context.Tickets.ToListAsync();
        }

        [HttpPost("checkmyticket")]
        public async Task<IActionResult> CheckMyTicket(Ticket ticket)
        {
            var isWin = await _context.Tickets.AnyAsync(x => x.Edition == ticket.Edition && x.Number == ticket.Number);
            return Ok(isWin);
        }
    }
}
