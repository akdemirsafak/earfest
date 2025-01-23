using earfest.Shared.Base;
using earPass.Domain.Entities;
using earPass.Domain.Enums;
using earPass.Domain.Models.Eventy;
using earPass.Domain.Repositories;
using earPass.Domain.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace earPass.Service.Services;

public sealed class EventyService : IEventyService
{
    private readonly IEventyRepository _eventRepository;

    public EventyService(IEventyRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<AppResult<EventResponse>> CreateAsync(EventRequest request)
    {
        //var @event = request.Adapt<Eventy>();
        var @event = new Eventy
        {
            Name = request.name,
            Description = request.description,
            Date = request.date,
            Location = request.location,
            Type = (EventTypeEnum)request.type,
            Image = request.image
        };
        await _eventRepository.AddAsync(@event);
        var eventResponse = @event.Adapt<EventResponse>();
        eventResponse.TypeId = (int)@event.Type;
        return AppResult<EventResponse>.Success(eventResponse);
    }

    public async Task<AppResult<bool>> DeleteAsync(string id)
    {
        var existEvent= await _eventRepository.GetByIdAsync(id);
        var result = await _eventRepository.DeleteAsync(existEvent);
        return AppResult<bool>.Success(result);
    }

   
    public async Task<AppResult<List<EventResponse>>> GetAllAsync()
    {
        var events = await _eventRepository.GetAsync();
        var eventsList = await events.ToListAsync();
        var eventsMapped = eventsList.Adapt<List<EventResponse>>();

        for (int i = 0; i < eventsList.Count; i++)
        {
            eventsMapped[i].TypeId = eventsList[i].Type != null ? (int)eventsList[i].Type : 0;
        }

        return AppResult<List<EventResponse>>.Success(eventsMapped);
    }



    public async Task<AppResult<EventResponse>> GetByIdAsync(string id)
    {
        var @event= await _eventRepository.GetByIdAsync(id);
        var eventResponse = @event.Adapt<EventResponse>();
        eventResponse.TypeId = (int)@event.Type;
        return AppResult<EventResponse>.Success(eventResponse);
    }

    public async Task<AppResult<EventResponse>> UpdateAsync(string id, EventRequest request)
    {
        var @event = await _eventRepository.GetByIdAsync(id);
        if (@event == null)
        {
            return AppResult<EventResponse>.Fail("Event not found");
        }
        @event.Name = request.name;
        @event.Description = request.description;
        @event.Date = request.date;
        @event.Location = request.location;
        @event.Type = (EventTypeEnum)request.type;
        @event.Image = request.image;

        var response = await _eventRepository.UpdateAsync(@event);
        var eventResponse = response.Adapt<EventResponse>();
        eventResponse.TypeId = (int)@event.Type;
        return AppResult<EventResponse>.Success(eventResponse);
    }
}
