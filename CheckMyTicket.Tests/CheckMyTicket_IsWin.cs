using System.Threading.Tasks;
using CheckMyTicket.Controllers;
using Xunit;
using CheckMyTicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using ConnectingApps.IntegrationFixture;
using Refit;
using CheckMyTicket.Services;

namespace CheckMyTicket.Tests
{
    public class CheckMyTicket_IsWin : IDisposable
    {
        private bool _disposed = false;
        protected readonly TicketContext _context;
        public CheckMyTicket_IsWin()
        {
            //Arrange - create In Memory Database
            var options = new DbContextOptionsBuilder<TicketContext>().UseInMemoryDatabase(databaseName: "TicketsDataBase").Options;
            //Create mocked Context
            _context = new TicketContext(options);
        }

        /// <summary>
        /// Метод тестирует результат
        /// </summary>
        [Theory]
        [InlineData("123", "A234D31", true)]
        [InlineData("123", "A234D32", true)]
        [InlineData("123", "A234D33", true)]
        [InlineData("121", "A234D31", false)]
        [InlineData("123", "A234D35", false)]
        [InlineData("121", "A234D35", false)]
        public async Task CheckMyTicket_CheckReturns(string edition, string number, bool expectedResult)
        {
            var ticketService = new TicketService(_context); 
            var ticketsController = new TicketsController(ticketService);
            var result = await ticketsController.CheckMyTicket(new Ticket{ Edition = edition, Number = number}) as ObjectResult;
            var actualResult = result.Value;
            Assert.Equal(expected: expectedResult, actual: actualResult);
        }
        /// <summary>
        /// Метод тестирует валидацию
        /// </summary>
        [Theory]
        [InlineData("1231", "A234D31", HttpStatusCode.BadRequest)]
        [InlineData("12", "A234D31", HttpStatusCode.BadRequest)]
        [InlineData("123", "A234D321", HttpStatusCode.BadRequest)]
        [InlineData("123", "A234D3", HttpStatusCode.BadRequest)]
        [InlineData("1231", "A234D331", HttpStatusCode.BadRequest)]
        [InlineData("12", "A234D3", HttpStatusCode.BadRequest)]
        [InlineData("123", "A234D31", HttpStatusCode.OK)]
        public async Task CheckMyTicket_CheckStatusCode(string edition, string number, HttpStatusCode expectedStatusCode)
        {
            using (var fixture = new RefitFixture<Startup, ILogicClient>(RestService.For<ILogicClient>))
            {
                var refitClient = fixture.GetRefitClient();
                var response = await refitClient.Post(new Ticket
                {
                    Edition = edition,
                    Number = number
                });

                var statusCode = response.StatusCode;

                Assert.Equal(expectedStatusCode, statusCode);
            }
        }

        public void Dispose() => Dispose(true);
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _context?.Dispose();
            }
            _disposed = true;
        }
    }
}
