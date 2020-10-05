using System.Threading.Tasks;
using NilDevStudio.Domain;

namespace NilDevStudio.Repository
{
    public interface INilDevRepository
    {
         // General
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
		 void DeleteRange<T>(T[] entity) where T : class;
         Task<bool> SaveChangesAsync();

         // Events
         Task<MyEvent[]> GetAllEvent(bool speaker);
         Task<MyEvent[]> GetAllEventByTheme(string theme, bool speaker);
         Task<MyEvent> GetEventById(int eventId, bool speaker);

         //Speaker
         Task<Speaker[]> GetAllSpeakersByName(string name, bool events);
         Task<Speaker> GetSpeaker(int speakerId, bool events);

    }
}
