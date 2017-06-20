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
    public partial class CampusPage : ContentPage
    {
        public CampusPage(Ticket newTicket)
        {
            InitializeComponent();
            BindingContext = new CampusVM(Navigation, this, newTicket);
        }
    }
}