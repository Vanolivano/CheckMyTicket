using System.Threading.Tasks;
using CheckMyTicket.Controllers;
using Xunit;
using CheckMyTicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace CheckMyTicket.Tests
{
    public class CheckMyTicket_IsWin
    {

        public CheckMyTicket_IsWin()
        {
           
           
        }

        [Fact]
        public async Task CheckMyTicket_Input_ReturnFalse()
        {
            //Arrange - create In Memory Database
            var options = new DbContextOptionsBuilder<TicketContext>().UseInMemoryDatabase(databaseName: "TicketsDataBase").Options;
            //Create mocked Context
            using (var context = new TicketContext(options))
            {
                context.Tickets.Add(new Ticket {Edition = "123", Number = "A234D31"});
                context.Tickets.Add(new Ticket {Edition = "123", Number = "A234D32"});
                context.Tickets.Add(new Ticket {Edition = "123", Number = "A234D33"});
                context.SaveChanges();
            }
            using (var context = new TicketContext(options))
            {
                var ticketsController = new TicketsController(context);
                var result = await ticketsController.CheckMyTicket(new Models.Ticket{ Edition = "111", Number = "1234567"}) as ObjectResult;
                var actualResult = result.Value;
                Assert.False((bool)actualResult, "Tiket 111-1234567 should not be win");
            }
        }
        [Fact]
        public async Task CheckMyTicket_Input_ReturnTrue()
        {
            //Arrange - create In Memory Database
            var options = new DbContextOptionsBuilder<TicketContext>().UseInMemoryDatabase(databaseName: "TicketsDataBase").Options;
            //Create mocked Context
            using (var context = new TicketContext(options))
            {
                context.Tickets.Add(new Ticket {Edition = "123", Number = "A234D31"});
                context.Tickets.Add(new Ticket {Edition = "123", Number = "A234D32"});
                context.Tickets.Add(new Ticket {Edition = "123", Number = "A234D33"});
                context.SaveChanges();
            }
            using (var context = new TicketContext(options))
            {
                var ticketsController = new TicketsController(context);
                var result = await ticketsController.CheckMyTicket(new Models.Ticket{ Edition = "123", Number = "A234D31"}) as ObjectResult;
                var actualResult = result.Value;
                Assert.True((bool)actualResult, "Tiket 123-A234D31 should be win");
            }
        }
    }
}
