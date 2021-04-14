using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IBlogService
    {
        Task SaveSingleAsync(Product product);
        Task<bool> SaveManyAsync(Product[] products);
        Task DeleteAsync(Product product);
    }
}
