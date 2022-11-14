using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.Entities.Event;
using codeKade.DataLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace codeKade.Application.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly IGenericRepository<Event> _eventRepository;

        public EventService(IGenericRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async ValueTask DisposeAsync()
        {
            if (_eventRepository != null)
            {
                await _eventRepository.DisposeAsync();
            }
        }

        public Task<List<Event>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Event> GetActiveEvent()
        {
            var ActiveEvent = await _eventRepository.GetEntityQuery().OrderByDescending(e => e.ID)
                .FirstOrDefaultAsync(e => e.IsActive);

            return ActiveEvent;
        }

        public async Task<List<Event>> GetNewEvents()
        {
            return await _eventRepository.GetEntityQuery().Take(6).ToListAsync();
        }

        public async Task<Event> GetSingleEvent(long id)
        {
            return await _eventRepository.GetByID(id);
        }
    }
}
