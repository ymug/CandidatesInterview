using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InterviewTest.App.Helpers;
using InterviewTest.App.Models;
using InterviewTest.App.Services;

namespace InterviewTest.App.ViewModels;

public partial class ProductListViewModel : ObservableObject
{
    private readonly IProductStore _productStore;
    private readonly List<IProduct> _products = new List<IProduct>();

    [ObservableProperty]
    private bool _isLoading;

    public ProductListViewModel(IProductStore productStore)
    {
        _productStore = productStore;
        _products.AddRange(_productStore.GetProducts());

        _productStore.ProductAdded += HandleProductAdded;
        _productStore.ProductRemoved += HandleProductRemoved;
        RefreshProducts();
    }


    public ObservableCollection<IProduct> ProductList { get; } = new([]);

    [RelayCommand]
    public void CheckProductAvailabilities()
    {
        List<ProductAvailabilityChecker> checkers = new List<ProductAvailabilityChecker>();
        List<Thread> t = new List<Thread>();
        foreach (IProduct p in _products)
        {
            ProductAvailabilityChecker productAvailabilityChecker = new ProductAvailabilityChecker(p);
            checkers.Add(productAvailabilityChecker);
            Thread thread = new Thread(productAvailabilityChecker.CheckIfAvailable);
            t.Add(thread);
            thread.Start();
        }
        foreach (Thread thread in t)
        {
            thread.Join();
        }

        StringBuilder sb = new StringBuilder();
        bool anyError = false;
        foreach (ProductAvailabilityChecker checker in checkers)
        {
            if (!checker.Result)
            {
                anyError = true;
                sb.AppendLine("The product " + checker.Product.Name + " is not available");
            }
        }
        if (!anyError)
        {
            //MessageBox.Show(this, "Everything is available.");
        }
        else
        {
            //MessageBox.Show(this, sb.ToString());
        }
    }


    private void RefreshProducts()
    {
        DispatcherHelpers.DispatchIfNecessary(() =>
        {
            IsLoading = true;

            try
            {
                ProductList.Clear();
                foreach (IProduct product in _products)
                {
                    ProductList.Add(product);
                }
            }
            finally
            {
                IsLoading = false;
            }
        });
    }


    private void HandleProductRemoved(Guid obj)
    {
        IProduct possibleProduct = _products.FirstOrDefault(p => p.Id == obj);
        if (possibleProduct != null)
        {
            _products.Remove(possibleProduct);
            RefreshProducts();
        }
    }

    private void HandleProductAdded(IProduct obj)
    {
        _products.Add(obj);
        RefreshProducts();
    }
}