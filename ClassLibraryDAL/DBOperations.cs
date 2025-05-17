using ClassLibraryEntities;
using Microsoft.EntityFrameworkCore;

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
         .AsNoTracking() // Important for fresh data
         .ToListAsync();
        }

        public async Task<bool> ProcessTransactionAsync(BusinessTransaction transaction)
        {
            using var transactionScope = await _context.Database.BeginTransactionAsync();

            try
            {
                // Generate invoice number
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

        private string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.Now:yyyyMMdd-HHmmss}-{Guid.NewGuid().ToString()[..4]}";
        }
        public async Task<List<BusinessModel>> GetAllBusinesses()
        {
            return await _context.Businesses.ToListAsync();
        }

        public async Task<bool> AddBusiness(BusinessModel business)
        {
            try
            {
                await _context.Businesses.AddAsync(business);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateBusiness(BusinessModel business)
        {
            try
            {
                _context.Businesses.Update(business);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteBusiness(int id)
        {
            try
            {
                var business = await _context.Businesses.FindAsync(id);
                if (business == null) return false;

                _context.Businesses.Remove(business);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<BusinessDetails> GetBusinessDetailsAsync()
        {
            return await _context.BusinessDetails.FirstOrDefaultAsync();
        }
    }
}
