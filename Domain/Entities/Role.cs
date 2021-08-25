using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }

        public ICollection<User> Users { get; private set; }

        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}
