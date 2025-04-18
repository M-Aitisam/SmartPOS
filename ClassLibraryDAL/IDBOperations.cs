﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryEntities;

namespace ClassLibraryDAL
{
    public interface IDBOperations
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<bool> ProcessTransactionAsync(BusinessTransaction transaction);
        // Existing methods...
    }
}
