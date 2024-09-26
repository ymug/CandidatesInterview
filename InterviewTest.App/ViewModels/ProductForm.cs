using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InterviewTest.App.Models;
using InterviewTest.App.Services;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace InterviewTest.App.ViewModels
{
    public partial class ProductForm : ObservableValidator
    {
        private static readonly Regex NumericalRegex = new Regex(@"^\d+$");

        private readonly IProductStore _productStore;

        [ObservableProperty]
        [Required]
        private string _name;

        [ObservableProperty]
        [Required]
        private ProductType _productType;

        [ObservableProperty]
        [Required]
        private int? _unitPrice;

        [ObservableProperty]
        [Required]
        private int? _quantity;

        public ProductForm(IProductStore productStore)
        {
            _productStore = productStore;
        }


        public IEnumerable<ProductType> ProductTypes => Enum.GetValues(typeof(ProductType)).Cast<ProductType>();


        [RelayCommand]
        private void Submit()
        {
            ValidateAllProperties();

            if (!HasErrors)
            {
                IProduct p;
                if (ProductType == Models.ProductType.Vegetable)
                {
                    p = new Vegetable(Name, Quantity!.Value, UnitPrice!.Value);
                }
                else
                {
                    p = new Fruit(Name, Quantity!.Value, UnitPrice!.Value);
                }
                _productStore.AddProduct(p);
            }
        }


        [RelayCommand]
        private void IsAllowedInput(object args)
        {
            if (args is not TextCompositionEventArgs textCompositionEventArgs)
            {
                return;
            }

            textCompositionEventArgs.Handled = !NumericalRegex.IsMatch(textCompositionEventArgs.Text);
        }
    }
}