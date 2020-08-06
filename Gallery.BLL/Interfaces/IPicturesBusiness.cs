using Gallery.Common;
using Gallery.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.BLL.Interfaces
{
    public interface IPicturesBusiness : IBaseBusiness<PictureModel>
    {
        Task<long> UploadAndInsert(PicturesDTO dto);
        Task<List<PicturesDTO>> Search();
    }
}
