using ClassLibraryEntities;

namespace ClassLibraryServices
{
    public interface IAdminService
    {
        event Action? OnChange;
        Task<List<BusinessModel>> GetAllBusinesses();
        Task<BusinessModel?> GetBusinessById(int id);
        Task<bool> AddBusiness(BusinessModel business);
        Task<bool> UpdateBusiness(BusinessModel business);
        Task<bool> DeleteBusiness(int id);
    }
}

