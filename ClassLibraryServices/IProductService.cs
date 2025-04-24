using ClassLibraryEntities;

namespace ClassLibraryServices
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
    }

}
