﻿using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.Common.Enums;
using Gallery.Common.Validations;
using Gallery.DAL;
using Gallery.DAL.Interfaces;
using Gallery.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        public Task Delete(long id) => 
            _picturesDataAccess.Delete(id);

        public Task<List<PictureModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PictureModel> GetById(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException("id", ErrorMessages.IdCanNotBeLowerThanOne);
            }

            PictureModel picture = await _picturesDataAccess.GetById(id);

            if (picture == null)
            {
                throw new ApplicationException(ErrorMessages.UserNotFound);
            }

            return picture;
        }

        public Task<long> Insert(PictureModel model)
        {
            throw new NotImplementedException();
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
                    Content =item.ImageName == null ? null : await GetImageContent(item.ImageName, item.Extension),
                    Extension = item.Extension
                };

                dtos.Add(dto);
            }

            return dtos;
        }

        public Task<bool> Update(PictureModel model)
        {
            throw new NotImplementedException();
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

        private async Task<Guid> UploadImage(string content, string extension)
        {
            if (string.IsNullOrEmpty(content))
                return new Guid();

            string folderName = Path.Combine("Resources", "Images");

            bool exists = System.IO.Directory.Exists(folderName);

            if (!exists)
                System.IO.Directory.CreateDirectory(folderName);

            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            Guid imageName = Guid.NewGuid();
            string imagePath = Path.Combine(pathToSave, imageName.ToString() + "." + extension);

            content =  content.Substring(content.LastIndexOf(",") + 1);

            byte[] image64 = Convert.FromBase64String(content);

            await File.WriteAllBytesAsync(imagePath, image64);

            return imageName;
        }

        private async Task<string> GetImageContent(Guid? imageName, string extension)
        {
            string content = null;
            string folderName = Path.Combine("Resources", "Images");
            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            string imagePath = Path.Combine(pathToSave, imageName.ToString() + "." + extension);

            if (File.Exists(imagePath))
            {
                Byte[] bytes = await File.ReadAllBytesAsync(imagePath);
                content = "data:image/jpeg;base64," + Convert.ToBase64String(bytes);
            }

            return content;
        }
    }
}