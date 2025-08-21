using POS_system_myowndesign.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows;

namespace POS_system_myowndesign.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const decimal TaxRate = 0.15m; // 15% Tax

        // --- Properties for Data Binding ---
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<OrderItem> Cart { get; set; }

        // Filtering properties
        private ObservableCollection<Product> _allProducts;
        private ICollectionView _filteredProducts;
        private string _currentFilter;
        public ICommand FilterByTypeCommand { get; }

        private string _selectedType;
        public string SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }


        public ICollectionView FilteredProducts
        {
            get => _filteredProducts;
            set
            {
                _filteredProducts = value;
                OnPropertyChanged(nameof(FilteredProducts));
            }
        }

        private decimal _subTotal;
        public decimal SubTotal
        {
            get => _subTotal;
            set { _subTotal = value; OnPropertyChanged(); }
        }

        private decimal _tax;
        public decimal Tax
        {
            get => _tax;
            set { _tax = value; OnPropertyChanged(); }
        }

        private decimal _totalPayable;
        public decimal TotalPayable
        {
            get => _totalPayable;
            set { _totalPayable = value; OnPropertyChanged(); }
        }

        private decimal _discountPercentage;
        public decimal DiscountPercentage
        {
            get => _discountPercentage;
            set { _discountPercentage = value; OnPropertyChanged(); UpdateTotals(); }
        }

        // --- Commands for Buttons ---
        public ICommand AddToCartCommand { get; }
        public ICommand DecrementItemCommand { get; }
        public ICommand ApplyDiscountCommand { get; }
        public ICommand CompleteOrderCommand { get; }

        // --- Constructor ---
        public MainViewModel()
        {
            // Initialize collections
            _allProducts = new ObservableCollection<Product>(ProductSamples.GetProducts());
            Products = new ObservableCollection<Product>(_allProducts); // Keep original reference if needed
            Cart = new ObservableCollection<OrderItem>();

            // Initialize Filtered Collection
            FilteredProducts = CollectionViewSource.GetDefaultView(_allProducts);
            FilteredProducts.Filter = item =>
            {
                if (string.IsNullOrEmpty(_currentFilter)) return true;
                return ((Product)item).Type.Equals(_currentFilter, StringComparison.OrdinalIgnoreCase);
            };

            // Initialize Commands
            FilterByTypeCommand = new RelayCommand(ExecuteFilterByType);
            AddToCartCommand = new RelayCommand(AddToCart);
            DecrementItemCommand = new RelayCommand(DecrementItem);
            ApplyDiscountCommand = new RelayCommand(UpdateTotals);
            CompleteOrderCommand = new RelayCommand(CompleteOrder);
        }

        // --- Command Methods ---
        private void AddToCart(object parameter)
        {
            if (parameter is Product product)
            {
                var existingItem = Cart.FirstOrDefault(item => item.Product.Name == product.Name);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    Cart.Add(new OrderItem { Product = product, Quantity = 1 });
                }
                UpdateTotals();
            }
        }

        private void DecrementItem(object parameter)
        {
            if (parameter is OrderItem item)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                }
                else
                {
                    Cart.Remove(item);
                }
                UpdateTotals();
            }
        }

        private void CompleteOrder(object parameter)
        {
            MessageBox.Show($"Order Completed!\nTotal Paid: {TotalPayable:C}");
            Cart.Clear();
            DiscountPercentage = 0;
            UpdateTotals();
        }

        private void UpdateTotals(object parameter = null)
        {
            SubTotal = Cart.Sum(item => item.TotalPrice);
            decimal discountAmount = SubTotal * (DiscountPercentage / 100);
            decimal discountedSubTotal = SubTotal - discountAmount;
            Tax = discountedSubTotal * TaxRate;
            TotalPayable = discountedSubTotal + Tax;
        }

        private void ExecuteFilterByType(object parameter)
        {
            _currentFilter = parameter as string;
            SelectedType = _currentFilter;
            FilteredProducts.Refresh();
        }
    }
}