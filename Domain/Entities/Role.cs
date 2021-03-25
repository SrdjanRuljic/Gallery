using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<User> Users { get; private set; }

        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}
