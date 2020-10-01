using System.Threading.Tasks;
using CheckMyTicket.Models;
using Refit;

namespace CheckMyTicket.Tests
{
    public interface ILogicClient
    {
        [Post("/api/Tickets/CheckMyTicket")]
        Task<ApiResponse<string>> Post([Body] Ticket ticket);
    }
}