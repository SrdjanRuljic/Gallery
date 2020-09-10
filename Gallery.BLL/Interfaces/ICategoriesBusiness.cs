using Gallery.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.BLL.Interfaces
{
    public interface ICategoriesBusiness : IBaseBusiness<CategoryModel>
    {
        Task<bool> Exists(string name, long id);
        Task<List<DropdownItemModel>> GetDropdownItems();
    }
}
