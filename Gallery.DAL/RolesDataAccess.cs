using Gallery.Common;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.DAL
{
    public class RolesDataAccess : IRolesDataAccess
    {
        private readonly DbContext _dBContext = new DbContext();

        public async Task<List<DropdownItemModel>> GetDropdownItems() =>
            await _dBContext.GetDropdownItems("[dbo].[sp_Roles.GetDropdownItems]");
    }
}
