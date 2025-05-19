using ClassLibraryEntities;

namespace ClassLibraryDAL
{
    public interface IDBOperations
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<BusinessDetails> GetBusinessDetailsAsync();
        Task<bool> ProcessTransactionAsync(BusinessTransaction transaction);
        Task<List<BusinessModel>> GetAllBusinesses();
        Task<bool> AddBusiness(BusinessModel business);
        Task<bool> UpdateBusiness(BusinessModel business);
        Task<bool> DeleteBusiness(int id);
        Task<BusinessModel?> GetBusinessByEmail(string email);
        Task<bool> BusinessExists(string email);
        // Existing methods...
    }
}
