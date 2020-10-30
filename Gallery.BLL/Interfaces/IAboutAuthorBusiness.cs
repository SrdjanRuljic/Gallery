using Gallery.DTO;
using System.Threading.Tasks;

namespace Gallery.BLL.Interfaces
{
    public interface IAboutAuthorBusiness
    {
        Task<long> UploadAndInsert(AboutAuthorDTO dto);
        Task<AboutAuthorDTO> GetById(long id);
        Task<bool> Update(AboutAuthorDTO dto);
    }
}
