using ClassLibraryEntities;

namespace ClassLibraryServices
{
    public interface IAdminService
    {
        Task<List<BusinessModel>> GetAllBusinesses();
        Task<bool> AddBusiness(BusinessModel business);
        Task<bool> UpdateBusiness(BusinessModel business);
        Task<bool> DeleteBusiness(int id);
        event Action OnChange;

        Task<BusinessDetails> GetBusinessDetails();
        Task<CurrentUser> GetCurrentUserAsync();

    }
}

