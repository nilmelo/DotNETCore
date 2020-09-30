using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using NilDevStudio.Domain.Identity;

namespace NilDevStudio.Domain
{
    public class Role : IdentityRole<int>
    {
		public List<UserRole> UserRoles { get; set; }
    }
}
