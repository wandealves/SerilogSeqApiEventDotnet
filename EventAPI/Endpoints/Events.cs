using EventAPI.Models;
using EventAPI.Services;

namespace EventAPI.Endpoints;

public static class Events
{
  public static void RegisterEndpoints(this IEndpointRouteBuilder routes)
  {
    var events = routes.MapGroup("/api/v1/events")
    .WithName("Events")
    .WithOpenApi();
    events.MapGet("", (IEventService service) => service.GetAll())
    .WithName("GetAllEvents")
    .WithTags("Events");
    events.MapGet("/{id}", (int id, IEventService service) => service.GetById(id))
    .WithName("GetEventById")
    .WithTags("Events");
    events.MapPost("", (Event newEvent, IEventService service) => service.Create(newEvent))
    .WithName("CreateEvent")
    .WithTags("Events");
    events.MapPut("/{id}", (int id, Event updatedEvent, IEventService service) =>
    {
      service.Update(id, updatedEvent);
    })
    .WithName("UpdateEvent")
    .WithTags("Events");
    events.MapDelete("/{id}", (int id, IEventService service) =>
    {
      service.Delete(id);
    })
    .WithName("DeleteEvent")
    .WithTags("Events");
  }
}