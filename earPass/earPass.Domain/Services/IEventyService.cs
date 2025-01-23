using earfest.Shared.Base;
using earPass.Domain.Models.Eventy;

namespace earPass.Domain.Services;

public interface IEventyService
{
    Task<AppResult<List<EventResponse>>> GetAllAsync();
    Task<AppResult<EventResponse>> GetByIdAsync(string id);
    Task<AppResult<EventResponse>> CreateAsync(EventRequest request);
    Task<AppResult<EventResponse>> UpdateAsync(string id, EventRequest request);
    Task<AppResult<bool>> DeleteAsync(string id);
}
