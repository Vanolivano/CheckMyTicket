using Microsoft.EntityFrameworkCore;

namespace CheckMyTicket.Models
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> TodoItems { get; set; }
    }
}