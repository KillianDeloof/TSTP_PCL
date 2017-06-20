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
    public partial class MessagePage : ContentPage
    {
        public MessagePage(Ticket newTicket)
        {
            InitializeComponent();

            BindingContext = new MessageVM(this, newTicket, btnSend);
        }
    }
}