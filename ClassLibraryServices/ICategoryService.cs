using ClassLibraryEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassLibraryServices
{
    public interface ICategoryService
    {
        Task<List<BusinessCategory>> GetCategoriesAsync();
        Task<bool> AddCategoryAsync(BusinessCategory category);
        Task<bool> AddSubItemAsync(BusinessSubItem subItem);
        Task<bool> DeleteCategoryAsync(int categoryId);
        Task<bool> DeleteSubItemAsync(int subItemId);
        Task UpdateCategoryAsync(BusinessCategory category);  // Single category update
        Task UpdateCategoriesAsync(List<BusinessCategory> categories);  // Multiple categories update
        Task<List<BusinessCategory>> GetCategoriesWithProductsAsync();
    }
}