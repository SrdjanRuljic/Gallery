﻿using Gallery.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.DAL.Interfaces
{
    public interface ICategoryDataAccess : IBaseDataAccess<CategoryModel>
    {
        Task<bool> Exists(string name, long id);
        Task<List<DropdownItemModel>> GetDropdownItems();
    }
}
