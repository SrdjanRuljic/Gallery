using System;

namespace Gallery.Common
{
    public class AboutAuthorModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public Guid? ImageName { get; set; }
        public string Extension { get; set; }
    }
}
