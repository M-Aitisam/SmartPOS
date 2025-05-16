using ClassLibraryEntities;

namespace ClassLibraryDAL
{
    public interface IDBOperations
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<bool> ProcessTransactionAsync(BusinessTransaction transaction);
        Task<List<BusinessModel>> GetAllBusinesses();
        Task<bool> AddBusiness(BusinessModel business);
        Task<bool> UpdateBusiness(BusinessModel business);
        Task<bool> DeleteBusiness(int id);
        // Existing methods...
    }
}
