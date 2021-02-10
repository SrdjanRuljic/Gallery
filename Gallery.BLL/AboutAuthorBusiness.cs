using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.Common.Helpers;
using Gallery.Common.Validations;
using Gallery.DAL;
using Gallery.DAL.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Gallery.BLL
{
    public class AboutAuthorBusiness : IAboutAuthorBusiness
    {
        private readonly IAboutAuthorDataAccess _aboutAuthorDataAccess;

        public AboutAuthorBusiness() =>
            _aboutAuthorDataAccess = new AboutAuthorDataAccess();

        public async Task<AboutAuthorModel> GetById(long id)
        {
            if (id <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);
            }

            AboutAuthorModel model = await _aboutAuthorDataAccess.GetById(id);

            model.Content = string.IsNullOrEmpty(model.Content) ? "/assets/images/no-image.png" : model.Content;

            if (model == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.AuthorNotFound);
            }

            return model;
        }

        public async Task<bool> Update(AboutAuthorModel model)
        {
            bool isUpdated = false;
            string errorMessage = null;

            if (!model.IsValid(out errorMessage))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }

            isUpdated = await _aboutAuthorDataAccess.Update(model);

            return isUpdated;
        }

        public async Task<long> Insert(AboutAuthorModel model)
        {
            long id = 0;
            string errorMessage = null;

            if (!model.IsValid(out errorMessage))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }

            id = await _aboutAuthorDataAccess.Insert(model);

            return id;
        }

        public Task<List<AboutAuthorModel>> GetAll() =>
            throw new System.NotImplementedException();

        public Task Delete(long id) =>
            throw new System.NotImplementedException();
    }
}
