using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibraryEntities;

namespace ClassLibraryServices
{
    public class AdminService : IAdminService
    {
        private readonly List<BusinessModel> _businesses = new();
        public event Action? OnChange;

        public async Task<List<BusinessModel>> GetAllBusinesses()
        {
            return await Task.FromResult(_businesses.ToList());
        }

        public async Task<BusinessModel?> GetBusinessById(int id)
        {
            return await Task.FromResult(_businesses.FirstOrDefault(b => b.BusinessID == id));
        }

        public async Task<bool> AddBusiness(BusinessModel business)
        {
            if (business == null) return false;

            business.BusinessID = _businesses.Count + 1;
            _businesses.Add(business);
            NotifyStateChanged();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateBusiness(BusinessModel business)
        {
            var existingBusiness = _businesses.FirstOrDefault(b => b.BusinessID == business.BusinessID);
            if (existingBusiness == null) return false;

            existingBusiness.BusinessName = business.BusinessName;
            existingBusiness.BusinessType = business.BusinessType;
            existingBusiness.City = business.City;
            existingBusiness.Area = business.Area;
            existingBusiness.Location = business.Location;
            existingBusiness.IsActive = business.IsActive;

            NotifyStateChanged();
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteBusiness(int id)
        {
            var business = _businesses.FirstOrDefault(b => b.BusinessID == id);
            if (business == null) return false;

            _businesses.Remove(business);
            NotifyStateChanged();
            return await Task.FromResult(true);
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
