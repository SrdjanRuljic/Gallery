using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.Common.Validations;
using Gallery.DAL;
using Gallery.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.BLL
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly ICategoryDataAccess _categoryDataAccess;

        public CategoryBusiness()
        {
            _categoryDataAccess = new CategoryDataAccess();
        }

        public async Task Delete(long id)
        {
            string errorMessage = null;

            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException("id", ErrorMessages.IdCanNotBeLowerThanOne);
            }

            try
            {
                await _categoryDataAccess.Delete(id);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("The DELETE statement conflicted with the REFERENCE"))
                    errorMessage = ErrorMessages.CanNotDeleteCategory;

                errorMessage = e.Message;

                throw new ApplicationException(errorMessage);
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
                throw new ArgumentOutOfRangeException("id", ErrorMessages.IdCanNotBeLowerThanOne);
            }

            CategoryModel model = await _categoryDataAccess.GetById(id);

            if (model == null)
            {
                throw new ApplicationException(ErrorMessages.CategoryNotFound);
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
                throw new ArgumentException(errorMessage);
            }

            bool categoryExists = await _categoryDataAccess.Exists(model.Name, id);

            if (!categoryExists)
            {
                id = await _categoryDataAccess.Insert(model);
            }
            else
            {
                throw new ArgumentException(ErrorMessages.CategoryExists);
            }

            return id;
        }

        public async Task<bool> Update(CategoryModel model)
        {
            bool isUpdated = false;
            string errorMessage = null;

            if (!model.IsValid(out errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            bool categoryExists = await _categoryDataAccess.Exists(model.Name, model.Id);

            if (!categoryExists)
            {
                isUpdated = await _categoryDataAccess.Update(model);
            }
            else
            {
                throw new ArgumentException(ErrorMessages.CategoryExists);
            }

            return isUpdated;
        }
    }
}
