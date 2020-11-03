using Gallery.BLL.Helpers;
using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.Common.Helpers;
using Gallery.Common.Validations;
using Gallery.DAL;
using Gallery.DAL.Interfaces;
using Gallery.DTO;
using System;
using System.Collections.Generic;
using System.Net;
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
            if (id <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);
            }

            PictureModel picture = await _picturesDataAccess.GetById(id);

            if (picture == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.PictureNotFound);
            }

            try
            {
                await _picturesDataAccess.Delete(id);

                ImageHelper.DeleteImage(picture.ImageName, picture.Extension);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<PicturesDTO>> Search(string name, long? categoryId)
        {
            List<PicturesDTO> dtos = new List<PicturesDTO>();

            if (String.IsNullOrEmpty(name))
                name = null;
            categoryId = categoryId == 0 ? null : categoryId;

            List<PictureModel> pictures = await _picturesDataAccess.Search(name, categoryId);

            foreach (var item in pictures)
            {
                string imageContext = await ImageHelper.GetImageContent(item.ImageName, item.Extension);

                PicturesDTO dto = new PicturesDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    CategoryId = item.CategoryId,
                    Description = item.Description,
                    Content = imageContext == null ? "/assets/images/no-image.png" : imageContext,
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
                ImageName = await ImageHelper.UploadImage(dto.Content, dto.Extension),
                Extension = dto.Extension
            };

            if (!model.IsValid(out errorMessage))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }

            try
            {
                id = await _picturesDataAccess.Insert(model);

            }
            catch (Exception exception)
            {
                ImageHelper.DeleteImage(model.ImageName, model.Extension);

                throw new Exception(exception.Message);
            }

            return id;
        }

        public async Task<PicturesDTO> GetById(long id)
        {
            if (id <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);
            }

            PictureDetailsModel picture = await _picturesDataAccess.GetSingleById(id);

            if (picture == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.PictureNotFound);
            }

            string imageContext = await ImageHelper.GetImageContent(picture.ImageName, picture.Extension);

            PicturesDTO dto = new PicturesDTO()
            {
                Id = picture.Id,
                Name = picture.Name,
                CategoryId = picture.CategoryId,
                Description = picture.Description,
                Content = imageContext == null ? "/assets/images/no-image.png" : imageContext,
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

            ImageHelper.DeleteImage(model.ImageName, model.Extension);

            model.CategoryId = dto.CategoryId;
            model.Name = dto.Name;
            model.Description = dto.Description;
            model.ImageName = await ImageHelper.UploadImage(dto.Content, dto.Extension);
            model.Extension = dto.Extension;

            if (!model.IsValid(out errorMessage))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }

            isUpdated = await _picturesDataAccess.Update(model);

            return isUpdated;
        }
    }
}
