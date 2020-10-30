using Gallery.BLL.Helpers;
using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.Common.Helpers;
using Gallery.Common.Validations;
using Gallery.DAL;
using Gallery.DAL.Interfaces;
using Gallery.DTO;
using System.Net;
using System.Threading.Tasks;

namespace Gallery.BLL
{
    public class AboutAuthorBusiness : IAboutAuthorBusiness
    {
        private readonly IAboutAuthorDataAccess _aboutAuthorDataAccess;

        public AboutAuthorBusiness() =>
            _aboutAuthorDataAccess = new AboutAuthorDataAccess();

        public async Task<AboutAuthorDTO> GetById(long id)
        {
            if (id <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);
            }

            AboutAuthorModel data = await _aboutAuthorDataAccess.GetById(id);

            if (data == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.AuthorNotFound);
            }

            string imageContext = await ImageHelper.GetImageContent(data.ImageName, data.Extension);

            AboutAuthorDTO dto = new AboutAuthorDTO()
            {
                Id = data.Id,
                Name = data.Name,
                Biography = data.Biography,
                Content = imageContext == null ? "/assets/images/no-image.png" : imageContext,
                Extension = data.Extension,
            };

            return dto;
        }

        public async Task<bool> Update(AboutAuthorDTO dto)
        {
            bool isUpdated = false;
            string errorMessage = null;

            AboutAuthorModel model = await _aboutAuthorDataAccess.GetById(dto.Id);

            ImageHelper.DeleteImage(model.ImageName, model.Extension);

            model.Name = dto.Name;
            model.Biography = dto.Biography;
            model.ImageName = await ImageHelper.UploadImage(dto.Content, dto.Extension);
            model.Extension = dto.Extension;

            if (!model.IsValid(out errorMessage))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }

            isUpdated = await _aboutAuthorDataAccess.Update(model);

            return isUpdated;
        }

        public async Task<long> UploadAndInsert(AboutAuthorDTO dto)
        {
            long id = 0;
            string errorMessage = null;

            AboutAuthorModel model = new AboutAuthorModel()
            {
                Name = dto.Name,
                Biography = dto.Biography,
                ImageName = await ImageHelper.UploadImage(dto.Content, dto.Extension),
                Extension = dto.Extension
            };

            if (!model.IsValid(out errorMessage))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }

            id = await _aboutAuthorDataAccess.Insert(model);

            return id;
        }
    }
}
