using Gallery.Common;
using Gallery.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.BLL.Interfaces
{
    public interface IPicturesBusiness
    {
        Task<long> UploadAndInsert(PicturesDTO dto);
        Task<List<PicturesDTO>> Search(string name, long categoryId);
        Task<PicturesDTO> GetById(long id);
        Task<bool> Update(PicturesDTO dto);
        Task Delete(long id);
    }
}
