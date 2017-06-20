using Microsoft.WindowsAzure.MobileServices;
using TSTP_PCL.Models;
using TSTP_PCL.Repositories;
using TSTP_PCL.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TSTP_PCL.ViewModels
{
    public partial class LoginVM : INotifyPropertyChanged
    {
        //private INavigationService _navigationService { get; }

        public LoginVM(LoginPage loginPage)
        {
            this.Navigation = loginPage.Navigation;
            this._btnLogin = loginPage.FindByName<Button>("btnLogin");

            LoginCommand = new Command(LoginClicked);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public Command LoginCommand { get; }
        private LoginRepository _loginRepo = new LoginRepository();
        private APIRepository _apiRepo = new APIRepository();
        public INavigation Navigation { get; set; }
        private Button _btnLogin = null;
        private Ticket _ticket;
        private DataBaseRepos _db = new DataBaseRepos("tstp");

        public async void LoginClicked()
        {
            ButtonModification();
            await StartLoginProcedure();
        }

        private async Task StartLoginProcedure()
        {
            // indien mogelijk user ophalen uit database
            // indien geslaagd -> tonen volgende pagina
            UserInfo ui = null;

            try
            {
                if (IsUserAuthenticated())
                {
                    ui = _db.GetUser(1);

                    if (ui == null)
                    {
                        ui = await _loginRepo.Login();
                        _db.CreateTable<UserInfo>();
                        _db.SaveItem<UserInfo>(ui);
                    }

                    _ticket.UserID = ui.ID;
                }
                else
                {
                    ui = await _loginRepo.Login();
                    _db.CreateTable<UserInfo>();
                    _db.SaveItem<UserInfo>(ui);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);

                ui = await _loginRepo.Login();
                _db.CreateTable<UserInfo>();
                _db.SaveItem<UserInfo>(ui);
            }

            if (ui != null)
            {
                _ticket = new Ticket(ui);
                AuthenticationDone();
            }
            else
            {
                await ShowLoginPage();
            }
        }

        private void ButtonModification()
        {
            // modify button when clicked
            _btnLogin.IsEnabled = false;
            _btnLogin.Text = "Loading ...";
            _btnLogin.TextColor = Xamarin.Forms.Color.White;

            // TO DO:
            // spinner gebruiken en knop disabelen
        }

        /// <summary>
        /// Opvragen of er een token aanwezig is in de database.
        /// </summary>
        /// <returns>bool</returns>
        private bool IsUserAuthenticated()
        {
            try
            {
                var auth = _db.GetAuthorization();

                if (auth == null)
                    return false;

                if (auth.Token != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task AuthenticationDone()
        {
            String token = _db.GetAuthorization().Token;
            int userId = _ticket.UserID;

            var mobileServiceClient = new MobileServiceClient("/api/userinfo");
            mobileServiceClient.CurrentUser = new MobileServiceUser(_ticket.UserID.ToString());
            mobileServiceClient.CurrentUser.MobileServiceAuthenticationToken = token;
            
            await LoadRooms();
            List<Room> roomList = (List<Room>)_db.GetItems<Room>();
            await ShowCategoryPage();
        }

        private async Task LoadRooms()
        {
            List<Room> roomList = await GetRoomList();
            _db.CreateTable<Room>();
            //roomList.ForEach(r => _db.SaveItem<Room>(r));
            foreach(Room room in roomList)
            {
                _db.SaveItem<Room>(room);
            }

            //List<Room> list = (List<Room>)_db.
        }

        private async Task<List<Room>> GetRoomList()
        {
            List<Room> roomList = await _apiRepo.GetRoomList();
            return roomList;
        }

        /// <summary>
        /// Tonen van de CategoryPage.
        /// </summary>
        private async Task ShowCategoryPage()
        {
            await Navigation.PushAsync(new CategoryPage(_ticket));
        }

        /// <summary>
        /// De LoginPage opnieuw inladen.
        /// </summary>
        private async Task ShowLoginPage()
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
