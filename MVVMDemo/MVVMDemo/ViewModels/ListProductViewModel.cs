using MVVMDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVMDemo.ViewModels
{
    public class ListProductViewModel : BaseViewModel
    {
        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(); }
        }

        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        public ICommand GoToDetailsCommand { private set; get; }

        public INavigation Navigation { get; set; }

        public ListProductViewModel(INavigation navigation)
        {
            Navigation = navigation;
            GoToDetailsCommand = new Command<Type>(async (pageType) => await GoToDetails(pageType));

            Products = new ObservableCollection<Product>();

            Products.Add(new Product() { ID = 1, Name = "Leche", Price = 10.30, IsAvailable = true });
            Products.Add(new Product() { ID = 2, Name = "Chocolates", Price = 12.78, IsAvailable = false });
            Products.Add(new Product() { ID = 3, Name = "Galletas", Price = 8, IsAvailable = true });
        }

        async Task GoToDetails(Type pageType)
        {
            if (SelectedProduct != null)
            {
                var page = (Page)Activator.CreateInstance(pageType);

                page.BindingContext = new ProductViewModel()
                {
                    IsAvailable = SelectedProduct.IsAvailable,
                    Name = SelectedProduct.Name,
                    Price = SelectedProduct.Price
                };

                await Navigation.PushAsync(page);
            }
        }

    }
}
