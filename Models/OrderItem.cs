using POS_system_myowndesign.ViewModels;
using System.ComponentModel;
using POS_system_myowndesign.Models;

namespace POS_system_myowndesign.Models
{
    public class OrderItem : BaseViewModel
    {
        public Product Product { get; set; }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public decimal TotalPrice => Product.Price * Quantity;
    }
}
