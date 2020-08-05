using Gallery.BLL.Interfaces;
using Gallery.Common.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.BLL
{
    public class PicturesBusiness : IPicturesBusiness
    {
        public PicturesBusiness()
        {
            
        }

        public async Task<int> UploadImage(string content, string extension)
        {
            if (string.IsNullOrEmpty(content))
                return (int)ImageEnum.NO_IMAGE;

            string folderName = Path.Combine("Resources", "Images");

            bool exists = System.IO.Directory.Exists(folderName);

            if (!exists)
                System.IO.Directory.CreateDirectory(folderName);

            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            Guid imageName = Guid.NewGuid();
            string imagePath = Path.Combine(pathToSave, imageName.ToString() + "." + extension);

            content =  content.Substring(content.LastIndexOf(",") + 1);

            byte[] image64 = Convert.FromBase64String(content);

            File.WriteAllBytes(imagePath, image64);

            return (int)ImageEnum.IMAGE_UPLOADED;
        }
    }
}
