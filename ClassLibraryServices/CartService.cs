using ClassLibraryDAL;
using ClassLibraryEntities;

namespace ClassLibraryServices
{
    public class CartService : ICartService
    {
        private readonly IDBOperations _dbOperations;

        public CartService(IDBOperations dbOperations)
        {
            _dbOperations = dbOperations;
        }

        public async Task<bool> ProcessTransactionAsync(BusinessTransaction transaction)
        {
            return await _dbOperations.ProcessTransactionAsync(transaction);
        }
    }
}
