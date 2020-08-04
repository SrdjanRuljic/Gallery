using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.WebAPI.Models
{
    public class PictureViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public string FileExtension { get; set; }
        public string FileContent { get; set; }
    }
}
