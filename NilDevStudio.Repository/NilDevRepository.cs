using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NilDevStudio.Domain;

namespace NilDevStudio.Repository
{
    public class NilDevRepository : INilDevRepository
    {
        public readonly NilDevContext _context;
        public NilDevRepository(NilDevContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackBehaviour = QueryTrackBehaviour.NoTracking;
        }

        // General
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
		public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        // Event
		public async Task<MyEvent[]> GetAllEvent(bool speakers = false)
        {
            IQueryable<MyEvent> query = _context.MyEvents
                .Include(c => c.Lots)
                .Include(c => c.SocialNetworks);

                if(speakers)
                {
                    query = query
                        .Include(es => es.EventSpeakers)
                        .ThenInclude(s => s.Speaker);
                }

                query = query.AsNoTracking()
                    .OrderBy(c => c.Id);

                return await query.ToArrayAsync();
        }

		public async Task<MyEvent[]> GetAllEventByTheme(string theme, bool speakers)
        {
            IQueryable<MyEvent> query = _context.MyEvents
                .Include(c => c.Lots)
                .Include(c => c.SocialNetworks);

                if(speakers)
                {
                    query = query
                        .Include(es => es.EventSpeakers)
                        .ThenInclude(s => s.Speaker);
                }

                query = query.AsNoTracking()
                    .OrderByDescending(c => c.DateEvent)
                    .Where(c => c.Theme.ToLower().Contains(theme.ToLower()));

                return await query.ToArrayAsync();
        }


        public async Task<MyEvent> GetEventById(int MyEventId, bool speakers)
        {
            IQueryable<MyEvent> query = _context.MyEvents
                .Include(c => c.Lots)
                .Include(c => c.SocialNetworks);

                if(speakers)
                {
                    query = query
                        .Include(es => es.EventSpeakers)
                        .ThenInclude(s => s.Speaker);
                }

                query = query
					.AsNoTracking()
                    .OrderBy(c => c.Id)
                    .Where(c => c.Id == MyEventId);

                return await query.FirstOrDefaultAsync();
        }

        // Speaker
        public async Task<Speaker> GetSpeaker(int SpeakerId, bool events = false)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(c => c.SocialNetworks);

                if(events)
                {
                    query = query
                        .Include(es => es.EventSpeakers)
                        .ThenInclude(e => e.MyEvent);
                }

                query = query.AsNoTracking()
                    .OrderBy(s => s.Name)
                    .Where(s => s.Id == SpeakerId);

                return await query.FirstOrDefaultAsync();
        }
        public async Task<Speaker[]> GetAllSpeakersByName(string name, bool events = false)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(c => c.SocialNetworks);

                if(events)
                {
                    query = query
                        .Include(es => es.EventSpeakers)
                        .ThenInclude(e => e.MyEvent);
                }

                query = query.AsNoTracking()
                    .Where(s => s.Name.ToLower().Contains(name.ToLower()));

                return await query.ToArrayAsync();
        }

    }
}
