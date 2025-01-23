using earfest.Shared.Base;
using earPass.Domain.Entities;
using earPass.Domain.Models.Ticket;
using earPass.Domain.Repositories;
using earPass.Domain.Services;
using Mapster;

namespace earPass.Service.Services;

public sealed class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IEventyRepository _eventRepository;

    public TicketService(ITicketRepository ticketRepository, IEventyRepository eventRepository)
    {
        _ticketRepository = ticketRepository;
        _eventRepository = eventRepository;
    }

    public async Task<AppResult<TicketResponse>> CreateAsync(TicketRequest request)
    {
        var @event = await _eventRepository.GetByIdAsync(request.EventId);
        var ticket = request.Adapt<Ticket>();
        ticket.Event = @event;
        await _ticketRepository.AddAsync(ticket);

        var response = ticket.Adapt<TicketResponse>();

        return AppResult<TicketResponse>.Success(response);
    }

    public async Task<AppResult<bool>> DeleteAsync(string id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);
        var result = await _ticketRepository.DeleteAsync(ticket);
        return AppResult<bool>.Success(result);

    }

    public async Task<AppResult<List<TicketResponse>>> GetAllAsync()
    {
        var tickets = await _ticketRepository.GetAsync();
        List<TicketResponse> response = tickets.Adapt<List<TicketResponse>>();

        return AppResult<List<TicketResponse>>.Success(response);
    }

    public async Task<AppResult<TicketResponse>> GetByIdAsync(string id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);
        var response = ticket.Adapt<TicketResponse>();
        return AppResult<TicketResponse>.Success(response);
    }

    public async Task<AppResult<TicketResponse>> UpdateAsync(string id, TicketRequest request)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        ticket.Name = request.Name;
        ticket.Description = request.Description;
        ticket.Price = request.Price;
        ticket.Quantity = request.Quantity;

        await _ticketRepository.UpdateAsync(ticket);
        TicketResponse response = ticket.Adapt<TicketResponse>();
        
        return AppResult<TicketResponse>.Success(response);
    }
}
