namespace Gallery.WebAPI.Models
{
    public class PictureDetailsViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Extension { get; set; }

        public virtual string Category { get; set; }
    }
}
