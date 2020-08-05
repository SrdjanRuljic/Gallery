using System;
using System.Collections.Generic;
using System.Text;

namespace Gallery.Common
{
    public class PictureModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public Guid? ImageName { get; set; }
        public string Extension { get; set; }
    }
}
