using Microsoft.EntityFrameworkCore;

namespace CheckMyTicket.Models
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options)
            : base(options)
        {
            base.Database.EnsureDeleted();
            base.Database.EnsureCreated(); // Создает базу если ее нет
        }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>().HasData(new Ticket {Id = 1, Edition = "123", Number = "A234D31"});
            modelBuilder.Entity<Ticket>().HasData(new Ticket {Id = 2, Edition = "321", Number = "A234D32"});
            modelBuilder.Entity<Ticket>().HasData(new Ticket {Id = 3, Edition = "123", Number = "A234D33"});
        }

        public DbSet<Ticket> Tickets { get; set; }
    }
}