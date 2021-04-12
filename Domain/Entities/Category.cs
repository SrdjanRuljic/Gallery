using System.Collections.Generic;

namespace Domain.Entities
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<Picture> Pictures { get; private set; }
        public ICollection<Product> Products { get; private set; }

        public Category()
        {
            Pictures = new HashSet<Picture>();
            Products = new HashSet<Product>();
        }
    }
}
