using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheckMyTicket.Models;
using CheckMyTicket.Services;

namespace CheckMyTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketsController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var result =  await _ticketService.GetAll();
            return Ok(result);
        }
        
        //POST: api/Tickets/CheckMyTicket
        [HttpPost("checkmyticket")]
        public async Task<IActionResult> CheckMyTicket(Ticket ticket)
        {
            var isWin = await _ticketService.CheckMyTicket(ticket);
            return Ok(isWin);
        }
    }
}
