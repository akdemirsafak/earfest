using earfest.Shared.Base;
using earPass.Domain.Models.Ticket;

namespace earPass.Domain.Services;

public interface ITicketService
{
    Task<AppResult<List<TicketResponse>>> GetAllAsync();
    Task<AppResult<TicketResponse>> GetByIdAsync(string id);
    Task<AppResult<TicketResponse>> CreateAsync(TicketRequest request);
    Task<AppResult<TicketResponse>> UpdateAsync(string id, TicketRequest request);
    Task<AppResult<bool>> DeleteAsync(string id);
}
