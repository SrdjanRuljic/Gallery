using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<Picture> Pictures { get; private set; }

        public Category()
        {
            Pictures = new HashSet<Picture>();
        }
    }
}
