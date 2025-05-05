using ClassLibraryEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassLibraryServices
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> AddProductAsync(Product product); // Updated return type
        Task<Product> UpdateProductAsync(Product product); // Updated return type
        Task<bool> DeleteProductAsync(int productId);
        Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
    }
}
