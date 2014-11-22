using System.Windows;
using System.Windows.Controls;
using _1000Best.ViewModels;

namespace _1000Best.Views
{
    public partial class SongView : UserControl
    {
        public SongView()
        {
            InitializeComponent();
        }

        private void PlaySpotify_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as SongViewModel;
            if (viewModel != null)
            {
                viewModel.PlaySpotify();
            }
        }

        private void PlayNokia_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as SongViewModel;
            if (viewModel != null)
            {
                viewModel.PlayNokia();
            }
        }

        private void PlayLastFm_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as SongViewModel;
            if (viewModel != null)
            {
                viewModel.PlayLastFm();
            }
        }
    }
}
