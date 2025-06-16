using ClassLibraryEntities;

namespace ClassLibraryServices
{
    public class StateContainerService
    {
        public event Action? OnChange;
        private List<BusinessCategory>? _categories;
        private List<Product>? _products;

        private bool _isRegistered;

        public bool IsRegistered
        {
            get => _isRegistered;
            set
            {
                if (_isRegistered != value)
                {
                    _isRegistered = value;
                    NotifyStateChanged();
                }
            }
        }
        private bool _isAuthenticated;

        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            set
            {
                if (_isAuthenticated != value)
                {
                    _isAuthenticated = value;
                    NotifyStateChanged();
                }
            }
        }
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

        private BusinessModel? _currentBusiness;

        public BusinessModel? CurrentBusiness
        {
            get => _currentBusiness;
            set
            {
                if (!Equals(_currentBusiness, value))
                {
                    _currentBusiness = value;
                    IsAuthenticated = value != null;
                    NotifyStateChanged();
                }
            }
        }
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
