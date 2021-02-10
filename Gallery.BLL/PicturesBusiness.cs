using Gallery.BLL.Helpers;
using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.Common.Helpers;
using Gallery.Common.Validations;
using Gallery.DAL;
using Gallery.DAL.Interfaces;
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

            PictureDetailsModel picture = await _picturesDataAccess.GetSingleById(id);

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

        public async Task<long> Insert(PictureModel model)
        {
            long id = 0;
            string errorMessage = null;

            if (!model.IsValid(out errorMessage))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }

            id = await _picturesDataAccess.Insert(model);

            return id;
        }
        public async Task<List<PictureModel>> Search(string name, long? categoryId)
        {
            if (String.IsNullOrEmpty(name))
                name = null;
            categoryId = categoryId == 0 ? null : categoryId;

            List<PictureModel> pictures = await _picturesDataAccess.Search(name, categoryId);

            foreach (var picture in pictures)
            {
                picture.Content = string.IsNullOrEmpty(picture.Content) ? "/assets/images/no-image.png" : picture.Content;
            }

            return pictures;
        }

        public async Task<PictureModel> GetById(long id)
        {
            if (id <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);
            }

            PictureModel model = await _picturesDataAccess.GetById(id);

            if (model == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.CategoryNotFound);
            }

            return model;
        }

        public async Task<bool> Update(PictureModel model)
        {
            bool isUpdated = false;
            string errorMessage = null;

            if (!model.IsValid(out errorMessage))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }

            isUpdated = await _picturesDataAccess.Update(model);

            return isUpdated;
        }

        public async Task<PictureDetailsModel> GetSingleById(long id)
        {
            if (id <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);
            }

            PictureDetailsModel model = await _picturesDataAccess.GetSingleById(id);

            model.Content = string.IsNullOrEmpty(model.Content) ? "/assets/images/no-image.png" : model.Content;

            if (model == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.CategoryNotFound);
            }

            return model;
        }

        public Task<List<PictureModel>> GetAll() =>
            throw new NotImplementedException();
    }
}
