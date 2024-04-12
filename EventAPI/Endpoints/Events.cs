using EventAPI.Models;
using EventAPI.Services;

public static class Events
{
  public static void RegisterEndpoints(this IEndpointRouteBuilder routes, EventService eventService)
  {
    var events = routes.MapGroup("/api/v1/events")
    .WithName("Events")
    .WithOpenApi();
    events.MapGet("", () => eventService.GetAllEvents())
    .WithName("GetAllEvents")
    .WithTags("Events");
    events.MapGet("/{id}", (int id) => eventService.GetEventById(id))
    .WithName("GetEventById")
    .WithTags("Events");
    events.MapPost("", (Event newEvent) => eventService.CreateEvent(newEvent))
    .WithName("CreateEvent")
    .WithTags("Events");
    events.MapPut("/{id}", (int id, Event updatedEvent) =>
    {
      eventService.UpdateEvent(id, updatedEvent);
    })
    .WithName("UpdateEvent")
    .WithTags("Events");
    events.MapDelete("/{id}", (int id) =>
    {
      eventService.DeleteEvent(id);
    })
    .WithName("DeleteEvent")
    .WithTags("Events");
  }
}