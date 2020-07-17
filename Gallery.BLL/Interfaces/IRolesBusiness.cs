using Gallery.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.BLL.Interfaces
{
    public interface IRolesBusiness
    {
        Task<List<DropdownItemModel>> GetDropdownItems();
    }
}
