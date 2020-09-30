using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NilDevStudio.Domain;
using NilDevStudio.Domain.Identity;

namespace NilDevStudio.Repository
{
    public class NilDevContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int> >
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
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<UserRole>(userRole =>
				{
					userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

					userRole.HasOne(ur => ur.Role)
						.WithMany(r => r.UserRoles)
						.HasForeignKey(ur => ur.RoleId)
						.IsRequired();

					userRole.HasOne(ur => ur.User)
						.WithMany(r => r.UserRoles)
						.HasForeignKey(ur => ur.UserId)
						.IsRequired();
				}
			);

            modelBuilder.Entity<EventSpeaker>()
                .HasKey(PE => new {PE.MyEventId, PE.SpeakerId});
        }
    }
}
