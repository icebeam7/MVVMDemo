using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MVVMDemo.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private double _price;

        public double Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }

        private bool _isAvailable;

        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { _isAvailable = value; OnPropertyChanged(); }
        }

        public ICommand ClearCommand { private set; get; }
        public ICommand SendEmailCommand { private set; get; }

        public ProductViewModel()
        {
            ClearCommand = new Command(() => Clear());
            SendEmailCommand = new Command(async () => await SendEmail());
        }

        void Clear()
        {
            Name = string.Empty;
            Price = 0;
            IsAvailable = false;
        }

        async Task SendEmail()
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = "New Product!",
                    Body = $"Product Name: {Name}\nPrice: {Price:N2}\nAvailability? {(IsAvailable ? "Yes" : "No")}",
                    To = new List<string> { "luis@luisbeltran.mx" }
                };

                await Email.ComposeAsync(message);
            }
            catch (Exception)
            {

            }
        }
    }
}
