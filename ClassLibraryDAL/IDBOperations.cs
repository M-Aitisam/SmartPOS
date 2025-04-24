using ClassLibraryEntities;

namespace ClassLibraryDAL
{
    public interface IDBOperations
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<bool> ProcessTransactionAsync(BusinessTransaction transaction);
        // Existing methods...
    }
}
