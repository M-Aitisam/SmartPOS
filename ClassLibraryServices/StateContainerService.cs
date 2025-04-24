using ClassLibraryEntities;

namespace ClassLibraryServices
{
    public class StateContainerService
    {
        public event Action? OnChange;
        private List<BusinessCategory>? _categories;
        private List<Product>? _products;

        public List<BusinessCategory> Categories
        {
            get => _categories ?? new();
            set
            {
                _categories = value;
                NotifyStateChanged();
            }
        }

        public List<Product> Products
        {
            get => _products ?? new();
            set
            {
                _products = value;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
