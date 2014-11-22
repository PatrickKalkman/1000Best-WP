using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Telerik.Windows.Controls;
using _1000Best.Model;
using _1000Best.Resources;
using _1000Best.ViewModels;

namespace _1000Best.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            LocalizationManager.GlobalResourceManager = AppResources.ResourceManager;
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
            InteractionEffectManager.AllowedTypes.Add(typeof(RadDataBoundListBoxItem));
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Songs.GroupPickerItemTap += Songs_GroupPickerItemTap;
            Artists.GroupPickerItemTap += ArtistsOnGroupPickerItemTap;
            Artists.SelectionChanged += Artists_SelectionChanged;
        }

        void Artists_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as MainPageViewModel;
            if (viewModel != null)
            {
                viewModel.SelectedArtist = (SongArtist)e.AddedItems[0];
            }
        }

        private void ArtistsOnGroupPickerItemTap(object sender, GroupPickerItemTapEventArgs e)
        {
            var viewModel = DataContext as MainPageViewModel;
            if (viewModel != null)
            {
                foreach (SongArtist artist in viewModel.Artists)
                {
                    if (artist.Name.Substring(0, 1).ToUpper().Equals(e.DataItem))
                    {
                        e.DataItemToNavigate = artist;
                        return;
                    }
                }
            }
        }

        void Songs_GroupPickerItemTap(object sender, Telerik.Windows.Controls.GroupPickerItemTapEventArgs e)
        {
            var viewModel = DataContext as MainPageViewModel;
            if (viewModel != null)
            {
                foreach (SongViewModel songViewModel in viewModel.Songs)
                {
                    if (songViewModel.Title.Substring(0, 1).ToUpper().Equals(e.DataItem))
                    {
                        e.DataItemToNavigate = songViewModel;
                        return;
                    }
                }
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            var askforReview = (bool)IsolatedStorageSettings.ApplicationSettings["askforreview"];
            if (askforReview)
            {
                IsolatedStorageSettings.ApplicationSettings["askforreview"] = false;
                var returnvalue = MessageBox.Show("Thank you for using 1000 Best, would you like to review this app?", "Please review my app", MessageBoxButton.OKCancel);
                if (returnvalue == MessageBoxResult.OK)
                {
                    var marketplaceReviewTask = new MarketplaceReviewTask();
                    marketplaceReviewTask.Show();
                }
            } 
            base.OnNavigatedTo(e);
        }
    }
}