using ClassLibraryEntities;

namespace ClassLibraryServices
{
    public interface ICartService
    {
        Task<bool> ProcessTransactionAsync(BusinessTransaction transaction);
    }
}
