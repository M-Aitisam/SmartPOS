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
    }
}
