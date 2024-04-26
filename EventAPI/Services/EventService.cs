using EventAPI.Models;

namespace EventAPI.Services;

public class EventService : IEventService
{
    private readonly ILogger<EventService> _logger;
    private List<Event> _events;

    public EventService(ILogger<EventService> logger)
    {
        _logger = logger;
        _events = new List<Event>();
    }

    public List<Event> GetAll()
    {
        return _events;
    }

    public Event? GetById(int id)
    {
        return _events.FirstOrDefault(e => e.Id == id);
    }

    public Event Create(Event newEvent)
    {
        _logger.LogInformation("Creating new event with title {title}", newEvent.Title);
        newEvent.Id = _events.Count > 0 ? _events.Max(e => e.Id) + 1 : 1;
        _events.Add(newEvent);
        return newEvent;
    }

    public void Update(int id, Event updatedEvent)
    {
        _logger.LogInformation("Updating event with id {id}", id);
        var existingEvent = _events.FirstOrDefault(e => e.Id == id);
        if (existingEvent != null)
        {
            existingEvent.Title = updatedEvent.Title;
            existingEvent.StartDateTime = updatedEvent.StartDateTime;
            existingEvent.EndDateTime = updatedEvent.EndDateTime;
        }
    }

    public void Delete(int id)
    {
        _logger.LogInformation("Deleting event with id {id}", id);
        var existingEvent = _events.FirstOrDefault(e => e.Id == id);
        if (existingEvent != null)
        {
            _events.Remove(existingEvent);
        }
    }
}
