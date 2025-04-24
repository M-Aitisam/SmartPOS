using ClassLibraryEntities;

namespace ClassLibraryServices
{
    public interface ICategoryService
    {
        Task<List<BusinessCategory>> GetCategoriesAsync();
        Task<bool> AddCategoryAsync(BusinessCategory category);
        Task<bool> AddSubItemAsync(BusinessSubItem subItem);
        Task<bool> DeleteCategoryAsync(int categoryId);
        Task<bool> DeleteSubItemAsync(int subItemId);

        Task UpdateCategoriesAsync(List<BusinessCategory> categories);
    }
}
