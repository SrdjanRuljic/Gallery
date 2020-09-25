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
    public class CategoriesBusiness : ICategoriesBusiness
    {
        private readonly ICategoriesDataAccess _categoryDataAccess;

        public CategoriesBusiness()
        {
            _categoryDataAccess = new CategoriesDataAccess();
        }

        public async Task Delete(long id)
        {
            string errorMessage = null;

            if (id <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);
            }

            try
            {
                await _categoryDataAccess.Delete(id);
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("The DELETE statement conflicted with the REFERENCE"))
                    errorMessage = ErrorMessages.CanNotDeleteCategory;
                else
                    errorMessage = exception.Message;

                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        public async Task<bool> Exists(string name, long id) =>
            await _categoryDataAccess.Exists(name, id);

        public async Task<List<CategoryModel>> GetAll() =>
            await _categoryDataAccess.GetAll();

        public async Task<CategoryModel> GetById(long id)
        {
            if (id <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);
            }

            CategoryModel model = await _categoryDataAccess.GetById(id);

            if (model == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.CategoryNotFound);
            }

            return model;
        }

        public Task<List<DropdownItemModel>> GetDropdownItems() =>
            _categoryDataAccess.GetDropdownItems();

        public async Task<long> Insert(CategoryModel model)
        {
            long id = 0;
            string errorMessage = null;

            if (!model.IsValid(out errorMessage))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }

            bool categoryExists = await _categoryDataAccess.Exists(model.Name, id);

            if (!categoryExists)
            {
                id = await _categoryDataAccess.Insert(model);
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.CategoryExists);
            }

            return id;
        }

        public async Task<bool> Update(CategoryModel model)
        {
            bool isUpdated = false;
            string errorMessage = null;

            if (!model.IsValid(out errorMessage))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }

            bool categoryExists = await _categoryDataAccess.Exists(model.Name, model.Id);

            if (!categoryExists)
            {
                isUpdated = await _categoryDataAccess.Update(model);
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.CategoryExists);
            }

            return isUpdated;
        }
    }
}
