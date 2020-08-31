using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.Common.Validations;
using Gallery.DAL;
using Gallery.DAL.Interfaces;
using Gallery.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Gallery.BLL
{
    public class PicturesBusiness : IPicturesBusiness
    {
        private readonly IPicturesDataAccess _picturesDataAccess;

        public PicturesBusiness()
        {
            _picturesDataAccess = new PicturesDataAcces();
        }

        public async Task Delete(long id)
        {
            PictureModel picture = await _picturesDataAccess.GetById(id);

            DeleteImage(picture.ImageName, picture.Extension);

            await _picturesDataAccess.Delete(id);
        }

        public async Task<List<PicturesDTO>> Search(string name, long categoryId)
        {
            List<PicturesDTO> dtos = new List<PicturesDTO>();

            List<PictureModel> pictures = await _picturesDataAccess.Search(name, categoryId);

            foreach (var item in pictures)
            {
                PicturesDTO dto = new PicturesDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    CategoryId = item.CategoryId,
                    Description = item.Description,
                    Content = item.ImageName == null ? null : await GetImageContent(item.ImageName, item.Extension),
                    Extension = item.Extension
                };

                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task<long> UploadAndInsert(PicturesDTO dto)
        {
            long id = 0;
            string errorMessage = null;

            PictureModel model = new PictureModel()
            {
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Description = dto.Description,
                ImageName = await UploadImage(dto.Content, dto.Extension),
                Extension = dto.Extension
            };

            if (!model.IsValid(out errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            id = await _picturesDataAccess.Insert(model);

            return id;
        }

        public async Task<PicturesDTO> GetById(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException("id", ErrorMessages.IdCanNotBeLowerThanOne);
            }

            PictureModel picture = await _picturesDataAccess.GetById(id);

            if (picture == null)
            {
                throw new ApplicationException(ErrorMessages.PictureNotFound);
            }

            PicturesDTO dto = new PicturesDTO()
            {
                Id = picture.Id,
                Name = picture.Name,
                CategoryId = picture.CategoryId,
                Description = picture.Description,
                Content = picture.ImageName == null ? null : await GetImageContent(picture.ImageName, picture.Extension),
                Extension = picture.Extension,
                Category = picture.Category
            };

            return dto;
        }

        public async Task<bool> Update(PicturesDTO dto)
        {
            bool isUpdated = false;
            string errorMessage = null;

            PictureModel model = await _picturesDataAccess.GetById(dto.Id);

            DeleteImage(model.ImageName, model.Extension);

            model.CategoryId = dto.CategoryId;
            model.Name = dto.Name;
            model.Description = dto.Description;
            model.ImageName = await UploadImage(dto.Content, dto.Extension);
            model.Extension = dto.Extension;

            if (!model.IsValid(out errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            isUpdated = await _picturesDataAccess.Update(model);

            return isUpdated;
        }

        #region [Image]

        private async Task<Guid> UploadImage(string content, string extension)
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

        private async Task<string> GetImageContent(Guid? imageName, string extension)
        {
            string content = null;
            string folderPath = Path.Combine("Resources", "Images");
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), folderPath);
            string imagePath = Path.Combine(directoryPath, imageName.ToString() + "." + extension);

            if (File.Exists(imagePath))
            {
                Byte[] bytes = await File.ReadAllBytesAsync(imagePath);
                content = "data:image/jpeg;base64," + Convert.ToBase64String(bytes);
            }

            return content;
        }

        private void DeleteImage(Guid? imageName, string extension)
        {
            string folderPath = Path.Combine("Resources", "Images");
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), folderPath);
            string imagePath = Path.Combine(directoryPath, imageName.ToString() + "." + extension);

            File.Delete(imagePath);
        }

        #endregion
    }
}
