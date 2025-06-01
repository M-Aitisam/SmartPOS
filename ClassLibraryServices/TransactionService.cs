// TransactionService.cs (in ClassLibraryServices)
using ClassLibraryDAL;
using ClassLibraryEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibraryServices
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveTransactionAsync(BusinessTransaction transaction)
        {
            try
            {
                transaction.Id ??= Guid.NewGuid().ToString();
                transaction.TransactionDate = DateTime.UtcNow;

                // Calculate totals if not already set
                if (transaction.SubTotal == 0)
                {
                    transaction.SubTotal = transaction.Items.Sum(i => i.UnitPrice * i.Quantity);
                }

                // Ensure discounts are properly tracked
                foreach (var discount in transaction.AppliedDiscounts)
                {
                    _context.Entry(discount).State = EntityState.Added;
                }

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving transaction: {ex.Message}", ex);
            }
        }

        public async Task<List<BusinessTransaction>> GetTransactionsByDateAsync(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var query = _context.Transactions
                    .Include(t => t.Items)
                    .AsQueryable();

                if (startDate.HasValue)
                    query = query.Where(t => t.TransactionDate >= startDate.Value);

                if (endDate.HasValue)
                    query = query.Where(t => t.TransactionDate <= endDate.Value);

                return await query
                    .OrderByDescending(t => t.TransactionDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving transactions: {ex.Message}", ex);
            }
        }

        public async Task<List<BusinessTransaction>> GetRecentTransactionsAsync()
        {
            try
            {
                return await _context.Transactions
                    .Include(t => t.Items)
                    .OrderByDescending(t => t.TransactionDate)
                    .Take(100)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving recent transactions: {ex.Message}", ex);
            }
        }

        public async Task<BusinessTransaction?> GetTransactionByIdAsync(string id)
        {
            try
            {
                return await _context.Transactions
                    .Include(t => t.Items)
                    .FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving transaction: {ex.Message}", ex);
            }
        }
        // TransactionService.cs
        public async Task<List<BusinessTransaction>> GetTransactionsAsync()
        {
            return await _context.Transactions
                .Include(t => t.Items)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
        public async Task<List<BusinessTransaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Transactions
                .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .Include(t => t.Items)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalSalesAsync()
        {
            return await _context.Transactions
                .Where(t => !t.IsReturn)
                .SumAsync(t => t.TotalAmount);
        }

        public async Task<decimal> GetTotalReturnsAsync()
        {
            return await _context.Transactions
                .Where(t => t.IsReturn)
                .SumAsync(t => t.TotalAmount);
        }

    }
}