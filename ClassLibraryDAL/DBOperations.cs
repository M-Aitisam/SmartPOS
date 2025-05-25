using ClassLibraryEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibraryDAL
{
    public class DBOperations : IDBOperations
    {
        private readonly AppDbContext _context;

        public DBOperations(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> ProcessTransactionAsync(BusinessTransaction transaction)
        {
            using var transactionScope = await _context.Database.BeginTransactionAsync();
            try
            {
                transaction.InvoiceNumber = GenerateInvoiceNumber();
                transaction.TransactionDate = DateTime.UtcNow;

                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();
                await transactionScope.CommitAsync();
                return true;
            }
            catch
            {
                await transactionScope.RollbackAsync();
                return false;
            }
        }

        public async Task<List<BusinessModel>> GetAllBusinesses()
        {
            return await _context.Businesses.ToListAsync();
        }

        public async Task<BusinessModel?> GetBusinessByEmail(string email)
        {
            return await _context.Businesses
                .FirstOrDefaultAsync(b => b.Email == email);
        }

        public async Task<bool> AddBusiness(BusinessModel business)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Businesses.AddAsync(business);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> UpdateBusiness(BusinessModel business)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Businesses.Update(business);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> DeleteBusiness(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var business = await _context.Businesses.FindAsync(id);
                if (business == null) return false;

                _context.Businesses.Remove(business);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<BusinessDetails> GetBusinessDetailsAsync()
        {
            return await _context.BusinessDetails.FirstOrDefaultAsync() ?? new BusinessDetails();
        }

        public async Task<bool> BusinessExists(string email)
        {
            return await _context.Businesses
                .AnyAsync(b => b.Email == email);
        }

        private string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.Now:yyyyMMdd-HHmmss}-{Guid.NewGuid().ToString()[..4]}";
        }

        public async Task<BusinessModel?> GetActiveBusinessAsync()
        {
            return await _context.Businesses
                .Include(b => b.BusinessDetails)
                .Include(b => b.GeneralInformation)
                .FirstOrDefaultAsync(b => b.IsActive);
        }
    }
}