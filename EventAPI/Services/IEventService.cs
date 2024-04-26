using EventAPI.Models;

namespace EventAPI.Services;

public interface IEventService
{
    List<Event> GetAll();
    Event? GetById(int id);
    Event Create(Event newEvent);
    void Update(int id, Event updatedEvent);
    void Delete(int id);
}
