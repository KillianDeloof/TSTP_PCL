using TSTP_PCL.Models;
using TSTP_PCL.Filters;
using TSTP_PCL.Repositories;
using TSTP_PCL.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace TSTP_PCL.ViewModels
{
    public class LocationSelectorVM : INotifyPropertyChanged
    {
        public LocationSelectorVM(INavigation navigation, Ticket ticket, LocationSelectorPage lsPage)
        {
            _locationSelectorPage = lsPage;

            this.Navigation = navigation;
            this._ticket = ticket;
            lsPage.Title = "GKG A (demo text)";


            Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private LocationSelectorPage _locationSelectorPage = null;
        private INavigation Navigation = null;
        private APIRepository _apiRepo = new APIRepository();
        private DataBaseRepos _db = new DataBaseRepos("tstp");
        private Ticket _ticket = null;

        private async Task<List<Floor>> GetFloorList()
        {
            List<Floor> floorList = new List<Floor>();

            //try
            //{
            //    floorList = (List<Floor>)_db.GetItems<Floor>();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
                floorList = await _apiRepo.GetFloorList();
            //    _db.CreateTable<Floor>();
            //    floorList.ForEach(f => _db.SaveItem<Floor>(f));
            //}

            return floorList;

            //-- dummy data --//

            //return new List<Floor>
            //{
            //    new Floor()
            //    {
            //        UDESC = "A"
            //    },
            //    new Floor()
            //    {
            //        UDESC = "B"
            //    },
            //    new Floor()
            //    {
            //        UDESC = "C"
            //    }
            //};
        }

        public List<Room> _roomList { get; set; }

        private async Task<List<Room>> FillRoomList()
        {
            //try
            //{
            //    _roomList = (List<Room>)_db.GetItems<Room>();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
                _roomList = await _apiRepo.GetRoomList();
            //}

            return _roomList;


            //-- dummy data --//

            //return new List<Room>
            //{
            //    new Room()
            //    {
            //        UDESC = "GKG.A.A.0.0.7"
            //    },

            //    new Room()
            //    {
            //        UDESC = "GKG.A.A.0.0.8"
            //    },

            //    new Room()
            //    {
            //        UDESC = "GKG.A.A.0.0.9"
            //    }
            //};
        }

        private void Start()
        {
            // ophalen van de gefilterde lijst
            // hierna wordt de accordion meteen ingeladen
            GetFilteredList();
        }

        private async Task GetFilteredList()
        {
            List<Floor> floorList = await GetFloorList();
            List<Room> roomList = await FillRoomList();

            List<Floor> filteredFloorList = new List<Floor>();

            try
            {
                filteredFloorList = floorList.ToList()
                    .Where(f => f.UCODE.Split('.')[0].ToLower() == _ticket.Building.UCODE.Split('.')[0].ToLower() &&
                           f.UCODE.Split('.')[1].ToLower() == _ticket.Building.UCODE.Split('.')[1].ToLower())
                    .ToList<Floor>();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            
            foreach (Floor floor in filteredFloorList)
            {
                List<Room> filteredList = new List<Room>();

                try
                {
                    filteredList = roomList
                        .Where(r => (r.UCODE.Split('.')[0] != null) &&
                            (r.UCODE.Split('.')[0].ToLower() == floor.UCODE.Split('.')[0].ToLower()) &&
                            (r.UCODE.Split('.')[1].ToLower() == floor.UCODE.Split('.')[1].ToLower()) &&
                            (r.UCODE.Replace(',', '.').Split('.')[3].ToLower() == floor.UCODE.Replace(',', '.').Split('.')[3].ToLower()))
                        .ToList<Room>();
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }

                CreateAccordeonItemView(filteredList, floor.UCODE.Replace(',', '.').Split('.')[3]);
            }
        }

        private void CreateAccordeonItemView(List<Room> roomList, String floorNumber)
        {
            if (roomList.Count == 0)
                return;

            var itemView = new Xamarin.CustomControls.AccordionItemView()
            {
                Text = "Floor " + floorNumber,
                ActiveTextColor = Xamarin.Forms.Color.White,
                TextColor = Xamarin.Forms.Color.White,
                BackgroundColor = new Xamarin.Forms.Color(
                    HexToRGB("45c8f5")[0],
                    HexToRGB("45c8f5")[1],
                    HexToRGB("45c8f5")[2], 1
                ),
                FontFamily= "VAG_Rounded_Bold",
                ButtonBackgroundColor = Color.FromRgb(69,200,245),
                ButtonActiveBackgroundColor = Color.FromRgb(52, 150, 184),
                TextPosition = Xamarin.CustomControls.TextPosition.Left,
                RightImage = "arrowRight",
                RotateImages = true
            };

            var stackLayout = new StackLayout()
            {
                //Padding = new Xamarin.Forms.Thickness(0.5, 0, 0.5, 0.5),
                BackgroundColor = Xamarin.Forms.Color.Black
            };

            var stackLayout2 = new StackLayout()
            {
                //Padding = new Xamarin.Forms.Thickness(5, 15),
                BackgroundColor = Color.FromRgb(224, 224, 224),
                Orientation = Xamarin.Forms.StackOrientation.Vertical
            };

            for (int i = 0; i < roomList.Count; i++)
            {
                var button = new Button()
                {
                    Text = roomList[i].UDESC.ToString(),
                    HorizontalOptions = Xamarin.Forms.LayoutOptions.Fill,
                    VerticalOptions = Xamarin.Forms.LayoutOptions.FillAndExpand,
                    BorderWidth = 0,
                    BorderColor = Xamarin.Forms.Color.Transparent,
                    BackgroundColor = Xamarin.Forms.Color.Transparent,
                    BorderRadius = 0,
                    Margin = 0
                };

                button.Clicked += ShowNextPage;
                stackLayout2.Children.Add(button);
            }

            

            stackLayout.Children.Add(stackLayout2);
            itemView.ItemContent = stackLayout;
            _locationSelectorPage.FindByName<AccordionView>("accordionView").Children.Add(itemView);
        }

        private void ShowNextPage(object sender, EventArgs e)
        {
            //_ticket.Building = 
            //Console.WriteLine("sender text: " + ((Button)sender).Text);
            _ticket.Location = _roomList.ToList<Room>().Where(r => r.UCODE.ToLower() == ((Button)sender).Text.ToLower()).FirstOrDefault();
            ShowMessagePage();
        }

        private async Task ShowMessagePage()
        {
            await Navigation.PushAsync(new MessagePage(_ticket));
        }

        /// <summary>
        /// Omzetten van hexadecimaal naar een rgb-array.
        /// Merk op dat enkel kleine letters kunnen gebruikt worden.
        /// </summary>
        /// <returns>double[]</returns>
        private double[] HexToRGB(String hex)
        {
            String hex1 = hex.Substring(0, 2);
            String hex2 = hex.Substring(2, 2);
            String hex3 = hex.Substring(4, 2);

            List<double> doubleList = new List<double>
            {
                Convert.ToInt32(hex1, 16),
                Convert.ToInt32(hex2, 16),
                Convert.ToInt32(hex3, 16)
            };

            return doubleList.ToArray<double>();
        }

        /* TO DO:
         * =======
         * - opvragen lijst met floors adhv geselecteerde campus & building
         * - gebruik van FloorFilter
         */
    }
}
