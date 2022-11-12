using codeKade.DataLayer.Entities.Event;

namespace codeKade.Application.Services.Interfaces;

public interface IEventService : IAsyncDisposable
{
    Task<List<Event>> GetAll();

    Task<Event> GetActiveEvent();

    Task<List<Event>> GetNewEvents();
}