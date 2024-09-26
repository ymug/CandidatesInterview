using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using InterviewTest.App.Helpers;
using InterviewTest.App.Messages;
using InterviewTest.App.Models;
using InterviewTest.App.Services;

namespace InterviewTest.App.ViewModels;

public partial class ProductListViewModel : ObservableObject
{
    private readonly IProductStore _productStore;
    private readonly IProductAvailabilityChecker _productAvailabilityChecker;
    private readonly List<IProduct> _products = new List<IProduct>();

    [ObservableProperty]
    private bool _isLoading;

    public ProductListViewModel(IProductStore productStore, IProductAvailabilityChecker productAvailabilityChecker)
    {
        _productStore = productStore;
        _productAvailabilityChecker = productAvailabilityChecker;
        _products.AddRange(_productStore.GetProducts());

        _productStore.ProductAdded += HandleProductAdded;
        _productStore.ProductRemoved += HandleProductRemoved;
        RefreshProducts();
    }


    public ObservableCollection<IProduct> ProductList { get; } = new([]);

    [RelayCommand]
    public void CheckProductAvailabilities()
    {
        Task.Run(async () =>
        {

            var products = _products.ToArray();

            var tasks = new Task<ProductAvailability>[products.Length];
            for (var i = 0; i < products.Length; i++)
            {
                tasks[i] = _productAvailabilityChecker.IsProductAvailableAsync(products[i]);
            }


            await Task.WhenAll(tasks);


            var availabilities = tasks.Select(t => t).Select(c => c.Result).ToArray();


            WeakReferenceMessenger.Default.Send(new ProductAvailabilitiesMessage(availabilities));

        });

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