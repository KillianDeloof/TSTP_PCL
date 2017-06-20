using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSTP_PCL.Models;
using TSTP_PCL.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSTP_PCL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        public CategoryPage(Ticket ticket)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            BindingContext = new CategoryVM(Navigation, this, ticket);
        }
    }
}