using EventAPI.Models;

namespace EventAPI.Services;

public class EventService
{
    private List<Event> _events;

    public EventService()
    {
        _events = new List<Event>();
    }

    public List<Event> GetAllEvents()
    {
        return _events;
    }

    public Event? GetEventById(int id)
    {
        return _events.FirstOrDefault(e => e.Id == id);
    }

    public Event CreateEvent(Event newEvent)
    {
        newEvent.Id = _events.Count > 0 ? _events.Max(e => e.Id) + 1 : 1;
        _events.Add(newEvent);
        return newEvent;
    }

    public void UpdateEvent(int id, Event updatedEvent)
    {
        var existingEvent = _events.FirstOrDefault(e => e.Id == id);
        if (existingEvent != null)
        {
            existingEvent.Title = updatedEvent.Title;
            existingEvent.StartDateTime = updatedEvent.StartDateTime;
            existingEvent.EndDateTime = updatedEvent.EndDateTime;
        }
    }

    public void DeleteEvent(int id)
    {
        var existingEvent = _events.FirstOrDefault(e => e.Id == id);
        if (existingEvent != null)
        {
            _events.Remove(existingEvent);
        }
    }
}
