using Microsoft.AspNetCore.Identity;
using NilDevStudio.Domain.Identity;


namespace NilDevStudio.Domain.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
		public User User { get; set; }
		public Role Role { get; set; }
    }
}
