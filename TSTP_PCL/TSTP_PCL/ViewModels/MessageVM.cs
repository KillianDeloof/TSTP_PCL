using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Plugin.Media.Abstractions;
using TSTP_PCL.Models;
using TSTP_PCL.Repositories;
using System.Threading.Tasks;
using TSTP_PCL.MobileSDK.AzureMobileClient;
using System.IO;
using Plugin.Media;
using TSTP_PCL.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TSTP_PCL.ViewModels
{
    public class MessageVM : INotifyPropertyChanged
    {
        public MessageVM(MessagePage messagePage, Ticket newTicket, Button btnSend)
        {
            this._messagePage = messagePage;
            this.Navigation = _messagePage.Navigation;
            this._ticket = newTicket;
            this._buttonSend = btnSend;

            if (_ticket.Location == null)
                _ticket.Location = new Room();

            if (_ticket.Location.UCODE == null)
                _ticket.Location.UCODE = "No Location selected";

            this._messagePage.FindByName<Button>("btnCategory").Text = _ticket.Category.ToString();
            this._messagePage.FindByName<Button>("btnLocation").Text = _ticket.Location.UCODE.ToString();

            MessageClickedCommand = new Command(MessageClicked);
            SendCommand = new Command(SendClicked);
            AttachCommand = new Command(AttachClicked);
            PictureCommand = new Command(PictureClicked);
            CategoryCommand = new Command(CategoryClicked);
            LocationCommand = new Command(LocationClicked);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private INavigation Navigation = null;
        private Ticket _ticket;
        private Room _r = new Room();
        private Button _buttonSend;
        private MessagePage _messagePage = null;

        public Command SendCommand { get; }
        public Command AttachCommand { get; }
        public Command PictureCommand { get; }
        public Command CategoryCommand { get; }
        public Command LocationCommand { get; }
        public Command AttachmentListCommand { get; }
        public Command MessageClickedCommand { get; }

        private String _message = "Type your message ...";
        public String Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                //_ticket.Message = _message;
            }
        }

        private String _subject = "Subject";
        public String Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
                //_ticket.Subject = _subject;
            }
        }

        private Attachment _selectedAttachment;
        public Attachment SelectedAttachment
        {
            get { return _selectedAttachment; }
            set {
                _selectedAttachment = value;
                DeleteAttachment();
                //PictureNameList = new ObservableCollection<Attachment>(_ticket.Attachments);
            }
        }

        private void SendClicked(object obj)
        {
            SendTicket();
        }

        private void AttachClicked(object obj)
        {
            PickPhoto();
        }

        private void MessageClicked()
        {
            Message = "";
        }

        private void LocationClicked(object obj)
        {
            ShowCampusPage();
        }

        private void CategoryClicked(object obj)
        {
            ShowCategoryPage();
        }

        private void PictureClicked(object obj)
        {
            TakePhoto();
        }

        /// <summary>
        /// Oproepen van de camera om een foto te nemen.
        /// </summary>
        /// <returns></returns>
        private async Task PickPhoto()
        {
            MediaFile photo = await MediaPicker.PickPhoto();
            bool ok = _ticket.AddAttachment(photo);
            if (ok == false)
            {
                await App.Current.MainPage.DisplayAlert("Failed!", "Size of atachments is to big!", "OK");
            }

            PictureNameList = new ObservableCollection<Attachment>(_ticket.Attachments);
        }
        
        /// <summary>
        /// Verwijderen van een Attachment.
        /// </summary>
        private async void DeleteAttachment()
        {
            if (_selectedAttachment == null)
                return;

            string action = await App.Current.MainPage.DisplayActionSheet("Photo Name", "Cancel", "Delete");

            if (action == "Delete")
            {
                _ticket.Attachments.Remove(_selectedAttachment);
                PictureNameList = new ObservableCollection<Attachment>(_ticket.Attachments);
            }
        }

        /// <summary>
        /// Nemen van een foto.
        /// </summary>
        /// <returns>Task</returns>
        private async Task TakePhoto()
        {
            MediaFile photo = await MediaPicker.TakePhoto();
            bool ok = _ticket.AddAttachment(photo);
            if (ok == false)
            {
                await App.Current.MainPage.DisplayAlert("Failed!", "Size of atachments is to big!", "OK");
            }

            PictureNameList = new ObservableCollection<Attachment>(_ticket.Attachments);
        }

        /// <summary>
        /// Verzenden van een ticket en tonen van een bevestiging.
        /// </summary>
        /// <returns>Task</returns>
        private async Task SendTicket()
        {
            if (!String.IsNullOrEmpty(_subject) && !String.IsNullOrEmpty(_message))
            {
                bool answer = await App.Current.MainPage.DisplayAlert("Send Ticket?", "Send Ticket?", "yes", "no");
                if (answer == true)
                {
                    _buttonSend.IsEnabled = false;
                    _ticket.FormatTicket(_subject, _message, _ticket.CatObj);
                    APIRepository apirepos = new APIRepository();
                    await apirepos.SendTicket(_ticket);
                    // displayAlert
                    await App.Current.MainPage.DisplayAlert("Ticket Send!", "The ticket has been send!", "OK");
                    _buttonSend.IsEnabled = true;

                    // refresh ticket
                    _ticket = new Ticket(_ticket.UserInfo);
                    // return to catogoryselector with the new ticket
                    await Navigation.PushAsync(new CategoryPage(_ticket));
                }
            }
            else
            {
                // wat te doen indien subject of message leeg is
                await App.Current.MainPage.DisplayAlert("No Subject", "Please fill in subject and message", "Ok");
                //Console.WriteLine("Subject of message is leeg.");
            }
        }

        private ObservableCollection<Attachment> _pictureNameList;
        public ObservableCollection<Attachment> PictureNameList
        {
            get
            {
                _pictureNameList = new ObservableCollection<Attachment>(_ticket.Attachments);
                return _pictureNameList;
            }
            set
            {
                if (_pictureNameList.Count != value.Count)
                {
                    _pictureNameList = value;

                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("PictureNameList"));
                }
            }
        }
        
        //private void OnPropertyChanged()
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs("PictureNameList"));
        //}

        private async Task ShowCampusPage()
        {
            await Navigation.PushAsync(new CampusPage(_ticket));
        }

        private async Task ShowCategoryPage()
        {
            await Navigation.PushAsync(new CategoryPage(_ticket));
        }

        //private void GetAttachmentNameList()
        //{
            //List<Attachment> attachmentNameList = new List<Attachment>();
            //_ticket.Attachments.ForEach(a => attachmentNameList.Add(a));

            //PictureNameList = attachmentNameList;


            //PictureNameList = new List<String>
            //{
            //    "photo01.jpg",
            //    "photo02.jpg",
            //    "photo03.jpg"
            //};
        //}

        // opvragen van locatie
        //private async Task GetPosition()
        //{
        //    double[] latlong = await GPSRepository.GetLocation();
        //    string location = "lat: " + latlong[0] + " / long: " + latlong[1];
        //}
    }
}
