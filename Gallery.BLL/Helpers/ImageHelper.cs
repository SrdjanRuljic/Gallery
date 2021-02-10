using System;
using System.IO;
using System.Threading.Tasks;

namespace Gallery.BLL.Helpers
{
    public static class ImageHelper
    {
        public static async Task<Guid> UploadImage(string content, string extension)
        {
            if (string.IsNullOrEmpty(content))
                return new Guid();

            string folderPath = Path.Combine("Resources", "Images");

            bool exists = System.IO.Directory.Exists(folderPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(folderPath);

            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), folderPath);
            Guid imageName = Guid.NewGuid();
            string imagePath = Path.Combine(directoryPath, imageName.ToString() + "." + extension);

            content = content.Substring(content.LastIndexOf(",") + 1);

            byte[] image64 = Convert.FromBase64String(content);

            await File.WriteAllBytesAsync(imagePath, image64);

            return imageName;
        }

        public static async Task<string> GetImageContent(Guid? imageName, string extension)
        {
            string content = null;
            string imagePath = CombineImagePath(imageName, extension);

            if (File.Exists(imagePath))
            {
                Byte[] bytes = await File.ReadAllBytesAsync(imagePath);
                content = "data:image/jpeg;base64," + Convert.ToBase64String(bytes);
            }

            return content;
        }

        public static void DeleteImage(Guid? imageName, string extension)
        {
            string imagePath = CombineImagePath(imageName, extension);

            File.Delete(imagePath);
        }

        public static string CombineImagePath(Guid? imageName, string extension)
        {
            string folderPath = Path.Combine("Resources", "Images");
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), folderPath);
            string imagePath = Path.Combine(directoryPath, imageName.ToString() + "." + extension);

            return imagePath;
        }
    }
}
