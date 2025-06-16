using ClassLibraryEntities;

namespace ClassLibraryServices
{
    public class StateContainerService
    {
        public event Action? OnChange;

        private BusinessModel? _currentBusiness;
        private List<BusinessCategory>? _categories;
        private List<Product>? _products;
        private bool _isRegistered;
        private bool _isAuthenticated;

        public BusinessModel? CurrentBusiness
        {
            get => _currentBusiness;
            set
            {
                if (_currentBusiness != value)
                {
                    _currentBusiness = value;
                    NotifyStateChanged();
                }
            }
        }

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

        public void ClearState()
        {
            CurrentBusiness = null;
            IsRegistered = false;
            IsAuthenticated = false;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

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
    }
}