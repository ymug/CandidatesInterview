using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InterviewTest.App.Models;
using InterviewTest.App.Services;

namespace InterviewTest.App.ViewModels
{
    public partial class ProductForm : ObservableValidator
    {
        private readonly IProductStore _productStore;

        [ObservableProperty]
        [Required]
        private string _name;

        [ObservableProperty]
        [Required]
        private ProductType _productType;

        [ObservableProperty]
        [Required]
        private int _unitPrice;

        [ObservableProperty]
        [Required]
        private int _quantity;

        public ProductForm(IProductStore productStore)
        {
            _productStore = productStore;
        }

        [RelayCommand]
        private void Submit()
        {
            ValidateAllProperties();

            if (!HasErrors)
            {
                IProduct p;
                if (ProductType == Models.ProductType.Vegetable)
                {
                    p = new Vegetable(Name, Quantity, UnitPrice);
                }
                else
                {
                    p = new Fruit(Name, Quantity, UnitPrice);
                }
                _productStore.AddProduct(p);
            }
        }
    }
}