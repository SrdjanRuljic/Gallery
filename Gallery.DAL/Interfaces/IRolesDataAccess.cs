using Gallery.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.DAL.Interfaces
{
    public interface IRolesDataAccess
    {
        Task<List<DropdownItemModel>> GetDropdownItems();
    }
}
