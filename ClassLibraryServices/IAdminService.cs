using ClassLibraryEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<bool> VerifyCredentials(string email, string password);
        Task<BusinessModel?> GetBusinessByEmail(string email);

        Task<BusinessModel?> GetCurrentBusiness();
        Task<byte[]?> GetBusinessLogo(string email);
    }
}