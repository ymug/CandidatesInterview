using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using InterviewTest.App.Helpers;
using InterviewTest.App.Messages;
using InterviewTest.App.Models;
using InterviewTest.App.Services;
using InterviewTest.App.ViewModels;

namespace InterviewTest.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
	{
        public MainWindow(IProductStore productStore, ProductFormViewModel productForm, ProductListViewModel productListViewModel)
        {
            ProductForm = productForm;
            ProductListViewModel = productListViewModel;

            InitializeComponent();


            WeakReferenceMessenger.Default.Register<ProductAvailabilitiesMessage>(this, (r, message) =>
            {
                DispatcherHelpers.DispatchIfNecessary(() => HandleProductAvailabilitiesMessage(message));
            });

}

        private void HandleProductAvailabilitiesMessage(ProductAvailabilitiesMessage message)
        {
            var notAvailableProducts = message.Value.Where(p => !p.IsAvailable).ToArray();


            if (notAvailableProducts.Length == 0)
            {
                MessageBox.Show(this, "Everything is available.");
            }
            else
            {
                var error = string.Join(
                    Environment.NewLine,
                    notAvailableProducts.Select(p => $"The product {p.Product.Name} is not available")
                );


                MessageBox.Show(this, error);
            }
        }

        public ProductFormViewModel ProductForm { get; }
        public ProductListViewModel ProductListViewModel { get; }


        public void Dispose()
        {
            WeakReferenceMessenger.Default.Unregister<ProductAvailabilitiesMessage>(this);
        }
    }
}
