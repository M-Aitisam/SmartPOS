// AdminService.cs
using ClassLibraryDAL;
using ClassLibraryEntities;

namespace ClassLibraryServices
{
    public class AdminService : IAdminService
    {
        private readonly IDBOperations _dbOperations;
        public event Action OnChange;

        public AdminService(IDBOperations dbOperations)
        {
            _dbOperations = dbOperations;
        }

        public async Task<List<BusinessModel>> GetAllBusinesses()
        {
            return await _dbOperations.GetAllBusinesses();
        }

        public async Task<bool> AddBusiness(BusinessModel business)
        {
            var result = await _dbOperations.AddBusiness(business);
            if (result) NotifyStateChanged();
            return result;
        }

        public async Task<bool> UpdateBusiness(BusinessModel business)
        {
            var result = await _dbOperations.UpdateBusiness(business);
            if (result) NotifyStateChanged();
            return result;
        }

        public async Task<bool> DeleteBusiness(int id)
        {
            var result = await _dbOperations.DeleteBusiness(id);
            if (result) NotifyStateChanged();
            return result;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}