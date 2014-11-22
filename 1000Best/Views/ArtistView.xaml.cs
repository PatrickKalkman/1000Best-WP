using System.Windows;
using Microsoft.Phone.Controls;
using _1000Best.ViewModels;

namespace _1000Best.Views
{
    public partial class ArtistView : PhoneApplicationPage
    {
        public ArtistView()
        {
            InitializeComponent();
            this.ArtistPicture.ImageOpened += ArtistPictureOnImageOpened;
            this.Loaded += ArtistView_Loaded;
        }

        private void ArtistPictureOnImageOpened(object sender, RoutedEventArgs routedEventArgs)
        {
            var viewModel = this.DataContext as ArtistViewModel;
            if (viewModel != null)
            {
                viewModel.ShowLoadingIndicator = false;
            }

            FadeInStoryBoard.Begin();
        }

        void ArtistView_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as ArtistViewModel;
            if (viewModel != null)
            {
                viewModel.ShowLoadingIndicator = true;
            }

            ArtistPicture.Opacity = 0;
        }
    }
}