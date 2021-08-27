using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class AppRole : IdentityRole
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
