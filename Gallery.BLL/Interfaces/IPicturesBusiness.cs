using Gallery.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.BLL.Interfaces
{
    public interface IPicturesBusiness : IBaseBusiness<PictureModel>
    {
        Task<List<PictureModel>> Search(string name, long? categoryId);
        Task<PictureDetailsModel> GetSingleById(long id);
    }
}
