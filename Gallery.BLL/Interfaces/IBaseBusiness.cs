using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.BLL.Interfaces
{
    public interface IBaseBusiness<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(long id);
        Task<long> Insert(T model);
        Task<bool> Update(T model);
        Task Delete(long id);
    }
}
