using Microsoft.EntityFrameworkCore;
using NilDevStudio.Domain;

namespace NilDevStudio.Repository
{
    public class NilDevContext : DbContext
    {
        public DbSet<MyEvent> MyEvents { get; set; }
        public DbSet<Speaker> Speakers { get; set; } 
        public DbSet<EventSpeaker> EventSpeakers { get; set; } 
        public DbSet<Lot> Lots { get; set; } 
        public DbSet<SocialNetwork> SocialNetworks { get; set; } 
        
        public NilDevContext(DbContextOptions<NilDevContext> options) : base(options)
        {
           
        }

        // Describing the relationship m to n
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventSpeaker>()
                .HasKey(PE => new {PE.EventId, PE.SpeakerId});
        }
    }
}