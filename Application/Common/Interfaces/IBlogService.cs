using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IBlogService
    {
        Task SaveSingleAsync(Product product);
        Task SaveManyAsync(Product[] products);
        Task DeleteAsync(Product product);
        Task SaveBulkAsync(Product[] products);
    }
}
