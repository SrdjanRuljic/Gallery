using Gallery.BLL.Interfaces;
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
    }
}
