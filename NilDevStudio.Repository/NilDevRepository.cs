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

            //
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

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        // Event
        public async Task<MyEvent> GetEventById(int eventId, bool speaker)
        {
            IQueryable<MyEvent> query = _context.MyEvents
                .Include(c => c.Lots)
                .Include(c => c.SocialNetworks);

                if(speaker)
                {
                    query = query
                        .Include(es => es.EventSpeaker)
                        .ThenInclude(s => s.Speaker);
                }

                query = query.AsNoTracking()
                    .OrderByDescending(c => c.DataEvent)
                    .Where(c => c.Id == eventId);

                return await query.FirstOrDefaultAsync();
        }

        public async Task<MyEvent[]> GetAllEvent(bool speaker = false)
        {
            IQueryable<MyEvent> query = _context.MyEvents
                .Include(c => c.Lots)
                .Include(c => c.SocialNetworks);

                if(speaker)
                {
                    query = query
                        .Include(es => es.EventSpeaker)
                        .ThenInclude(s => s.Speaker);
                }

                query = query.AsNoTracking()
                    .OrderByDescending(c => c.DataEvent);

                return await query.ToArrayAsync();
        }

        public async Task<MyEvent[]> GetAllEventByTheme(string theme, bool speaker)
        {
            IQueryable<MyEvent> query = _context.MyEvents
                .Include(c => c.Lots)
                .Include(c => c.SocialNetworks);

                if(speaker)
                {
                    query = query
                        .Include(es => es.EventSpeaker)
                        .ThenInclude(s => s.Speaker);
                }

                query = query.AsNoTracking()
                    .OrderByDescending(c => c.DataEvent)
                    .Where(c => c.Theme.ToLower().Contains(theme.ToLower()));

                return await query.ToArrayAsync();
        }

        // Speaker


        public async Task<Speaker> GetSpeaker(int speakerId, bool events = false)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(c => c.SocialNetworks);

                if(events)
                {
                    query = query
                        .Include(es => es.EventSpeaker)
                        .ThenInclude(e => e.Event);
                }

                query = query.AsNoTracking()
                    .OrderBy(s => s.Name)
                    .Where(s => s.Id == speakerId);

                return await query.FirstOrDefaultAsync();
        }
        public async Task<Speaker[]> GetAllSpeakersByName(string name, bool events = false)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(c => c.SocialNetworks);

                if(events)
                {
                    query = query
                        .Include(es => es.EventSpeaker)
                        .ThenInclude(e => e.Event);
                }

                query = query.AsNoTracking()
                    .Where(s => s.Name.ToLower().Contains(name.ToLower()));

                return await query.ToArrayAsync();
        }

    }
}
