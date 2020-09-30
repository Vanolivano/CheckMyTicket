using System.Threading.Tasks;
using CheckMyTicket.Controllers;
using Xunit;
using System.Text.Json;
using Moq;
using CheckMyTicket.Models;
using CheckMyTicket.Test;

namespace CheckMyTicket.Tests
{
    public class CheckMyTicket_IsWin
    {
        private readonly TicketsController _ticketsController;

        public CheckMyTicket_IsWin()
        {
            var mock = new Mock<TicketContext>();
            mock.Setup(x => x.Set<Ticket>())
                .Returns(new FakeDbSet<Ticket>
                {
                    new Ticket {Id = 1, Edition = "123", Number = "A234D31"},
                    new Ticket {Id = 2, Edition = "123", Number = "A234D32"},
                    new Ticket {Id = 3, Edition = "123", Number = "A234D33"}
                });
            _ticketsController = new TicketsController(mock.Object);
        }

        [Fact]
        public async Task CheckMyTicket_InputIs1_ReturnFalse()
        {
            var result = await _ticketsController.CheckMyTicket(new Models.Ticket{ Edition = "111", Number = "1234567"});
            var isResult = JsonSerializer.Deserialize<bool>(result.ToString());
            Assert.False(isResult, "1 should not be prime");
        }
    }
}
