using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSTP_PCL.ViewModels;
using TSTP_PCL.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSTP_PCL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationSelectorPage : ContentPage
    {
        public LocationSelectorPage(Ticket ticket)
        {
            InitializeComponent();

            //this._ticket = ticket;
            BindingContext = new LocationSelectorVM(Navigation, ticket, this);
            //Start();
        }
    }
}