using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using InterviewTest.App.Models;
using InterviewTest.App.Services;
using InterviewTest.App.ViewModels;

namespace InterviewTest.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
        public MainWindow(IProductStore productStore, ProductForm productForm, ProductListViewModel productListViewModel)
        {
            ProductForm = productForm;
            ProductListViewModel = productListViewModel;

            InitializeComponent();
		}

        public ProductForm ProductForm { get; }
        public ProductListViewModel ProductListViewModel { get; }


        private void _unitprice_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex(@"^\d+$");
			e.Handled = !regex.IsMatch(e.Text);
		}


		private void _quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex(@"^\d+$");
			e.Handled =!regex.IsMatch(e.Text);
		}
	}
}
