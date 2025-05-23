﻿// ITransactionService.cs (in ClassLibraryServices)
using ClassLibraryEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassLibraryServices
{
    public interface ITransactionService
    {
        Task SaveTransactionAsync(BusinessTransaction transaction);
        Task<List<BusinessTransaction>> GetTransactionsByDateAsync(DateTime? startDate, DateTime? endDate);
        Task<List<BusinessTransaction>> GetRecentTransactionsAsync();
        Task<BusinessTransaction?> GetTransactionByIdAsync(string id);
        Task<List<BusinessTransaction>> GetTransactionsAsync();

        Task<List<BusinessTransaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalSalesAsync();
        Task<decimal> GetTotalReturnsAsync();
    }
}