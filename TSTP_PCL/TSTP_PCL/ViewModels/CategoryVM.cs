using TSTP_PCL.Models;
using TSTP_PCL.Repositories;
using TSTP_PCL.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TSTP_PCL.ViewModels
{
    public class CategoryVM : INotifyPropertyChanged
    {
        public CategoryVM(INavigation navigation, CategoryPage categoryPage, Ticket ticket)
        {
            this.Navigation = navigation;
            this._categoryPage = categoryPage;
            this._ticket = ticket;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private APIRepository _dataRepo = new APIRepository();
        INavigation Navigation = null;
        private Ticket _ticket;
        private CategoryPage _categoryPage = null;

        // wordt opgevuld met de geselecteerde MainCategory
        private MainCategory _selectedCategory;
        public MainCategory SelectedCategory
        {
            get { return _selectedCategory; }
            set {
                _selectedCategory = value;
                ShowCategoryPage(_selectedCategory.SubCategoryList);
            }
        }

        // lijst van categorieën die worden opgevraagd wanneer de lijst wordt geladen
        // inladen gebeurt in GetCategoryList()
        private ObservableCollection<MainCategory> _categoryList = null;
        public ObservableCollection<MainCategory> CategoryList
        {
            get
            {
                if (_categoryList == null)
                    GetCategoryList();

                return _categoryList;
            }
            set
            {
                _categoryList = value;
            }
        }

        private void GetCategoryList()
        {
            CategoryList = new ObservableCollection<MainCategory>(_dataRepo.GetHardCodedCategoryList());

            //List<String> catStringList = new List<String>
            //    {
            //        "Campus",
            //        "Faciliteiten & diensten",
            //        "Lesmateriaal",
            //        "Netwerk",
            //        "Software & hardware",
            //        "Organisatie",
            //        "Overige"
            //    };

            List<String> categoryPictureList = new List<String>()
            {
                "ic_location_city_black_24dp.png",
                "ic_directions_bus_black_24dp.png",
                "ic_book_black_24dp.png",
                "ic_settings_input_hdmi_black_24dp.png",
                "ic_laptop_black_24dp.png",
                "ic_people_black_24dp.png",
                "ic_priority_high_black_24dp.png"
            };

            //for (int i = 0; i < catStringList.Count; i++)
            //{
            //    categoryList.Add(new MainCategory()
            //    {
            //        CategoryUDesc = catStringList[i],
            //        Picture = categoryPictureList[i],
            //        Subtitle = "Subtitle test " + i
            //    });
            //}

            //CategoryList = new ObservableCollection<MainCategory>(categoryList);
        }

        //private List<MainCategory> GetCategoryStringList()
        //{
        //    List<MainCategory> categoryList = new List<MainCategory>();

        //    List<String> catStringList = new List<String>
        //    {
        //        "Campus",
        //        "Faciliteiten & diensten",
        //        "Lesmateriaal",
        //        "Netwerk",
        //        "Software & hardware",
        //        "Overige"
        //    };

        //    List<String> categoryPictureList = new List<String>()
        //    {
        //        "ic_location_city_black_24dp.png",
        //        "ic_directions_bus_black_24dp.png",
        //        "ic_book_black_24dp.png",
        //        "ic_settings_input_hdmi_black_24dp.png",
        //        "ic_laptop_black_24dp.png",
        //        "ic_priority_high_black_24dp.png"
        //    };

        //    List<MainCategory> categoryList = new List<MainCategory>()
        //    {

        //    };

        //    for (int i = 0; i < catStringList.Count; i++)
        //    {
        //        categoryList.Add(new MainCategory()
        //        {
        //            CategoryUDesc = catStringList[i],
        //            Picture = categoryPictureList[i],
        //            Subtitle = "Subtitle test " + i
        //            //SubCategoryList
        //        });
        //    }

        //    return categoryList;
        //}

        /// <summary>
        /// Tonen van de SubCategoryPage als de geselecteerde Category niet null is.
        /// </summary>
        private async Task ShowCategoryPage(List<Category> subCategoryList)
        {
            _categoryPage.FindByName<ListView>("listViewCategory").SelectedItem = null;

            if (subCategoryList != null)
            {
                _selectedCategory = null;
                await Navigation.PushAsync(new SubCategoryPage(subCategoryList, _ticket));
            }
        }
    }
}
