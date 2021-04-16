using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IBlogService
    {
        Task SaveSingleAsync(Product product);
        Task<string> SaveManyAsync(Product[] products);
        Task DeleteAsync(Product product);
        Task<string> SaveBulkAsync(Product[] products);
    }
}
