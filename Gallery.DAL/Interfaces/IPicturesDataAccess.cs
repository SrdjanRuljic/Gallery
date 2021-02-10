using Gallery.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.DAL.Interfaces
{
    public interface IPicturesDataAccess : IBaseDataAccess<PictureModel>
    {
        Task<List<PictureModel>> Search(string name, long? categoryId);
        Task<PictureDetailsModel> GetSingleById(long id);
    }
}
