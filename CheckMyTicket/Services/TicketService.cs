using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CheckMyTicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CheckMyTicket.Services
{
    public class TicketService
    {
        private readonly TicketContext _ticketContext;
        private readonly IMemoryCache _cache;

        public TicketService(TicketContext ticketContext, IMemoryCache cache)
        {
            _ticketContext = ticketContext;
            _cache = cache;
        }
        public TicketService(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }
        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _ticketContext.Tickets.ToListAsync();
        }

        public async Task<bool> CheckMyTicket(Ticket ticket)
        {
            Ticket cacheTicket;
            if(_cache != null)
            {
                _cache.TryGetValue<Ticket>(ticket.ToString(), out cacheTicket);
                return cacheTicket != null;
            }
            var isResult = await _ticketContext.Tickets.AnyAsync(x => x.Edition == ticket.Edition && x.Number == ticket.Number);
            if(_cache != null && isResult)
            {
                if (!_cache.TryGetValue(ticket.ToString(), out cacheTicket))
                {
                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromSeconds(3));

                    // Save data in cache.
                    _cache.Set<Ticket>(ticket.ToString(), ticket, cacheEntryOptions);
                }
            }
            return isResult;
        }
    }
    
}