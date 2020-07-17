using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.DAL;
using Gallery.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.BLL
{
    public class RolesBusiness : IRolesBusiness
    {
        private readonly IRolesDataAccess _rolesDataAccess;

        public RolesBusiness()
        {
            _rolesDataAccess = new RolesDataAccess();
        }

        public async Task<List<DropdownItemModel>> GetDropdownItems() =>
            await _rolesDataAccess.GetDropdownItems();
    }
}
