using ClassLibraryDAL;
using ClassLibraryEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassLibraryServices
{
    public class AdminService : IAdminService
    {
        private readonly IDBOperations _dbOperations;
       

        public event Action OnChange;

        public async Task<BusinessModel?> GetCurrentBusiness()
        {
            return await _dbOperations.GetActiveBusinessAsync();
        }

        public AdminService(IDBOperations dbOperations)
        {
            _dbOperations = dbOperations;
        }

        public async Task<BusinessDetails> GetBusinessDetails()
        {
            return await _dbOperations.GetBusinessDetailsAsync();
        }

        public async Task<List<BusinessModel>> GetAllBusinesses()
        {
            return await _dbOperations.GetAllBusinesses();
        }

        public async Task<bool> AddBusiness(BusinessModel business)
        {
            try
            {
                if (await _dbOperations.BusinessExists(business.Email))
                {
                    return false;
                }

                var result = await _dbOperations.AddBusiness(business);
                if (result) NotifyStateChanged();
                return result;
            }
            catch
            {
                return false;
            }
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

        public async Task<CurrentUser> GetCurrentUserAsync()
        {
            return await Task.FromResult(new CurrentUser { Id = "3", Name = "Cashier" });
        }

        public async Task<BusinessModel?> GetBusinessByEmail(string email)
        {
            return await _dbOperations.GetBusinessByEmail(email);
        }

        public async Task<bool> VerifyCredentials(string email, string password)
        {
            var business = await GetBusinessByEmail(email);
            if (business == null) return false;

            return PasswordHasher.VerifyPassword(password, business.PasswordHash);
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}