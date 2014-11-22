using System.Collections.ObjectModel;
using System.Windows.Media;
using Caliburn.Micro;
using _1000Best.Common;
using _1000Best.Model;
using _1000Best.Resources;

namespace _1000Best.ViewModels
{
    public class YearViewModel : Screen
    {
        private readonly SongManager songManager;
        private readonly BackgroundImageBrush backgroundImageBrush;
        
        public YearViewModel(BackgroundImageBrush backgroundImageBrush, SongManager songManager)
        {
            this.backgroundImageBrush = backgroundImageBrush;
            this.songManager = songManager;
        }

        public ImageBrush BackgroundImageBrush
        {
            get { return backgroundImageBrush.GetBackground(); }
        }

        protected override void OnInitialize()
        {
            Year selectedYear = songManager.GetYear(year);
            Songs = new ObservableCollection<SongViewModel>(SongMapper.Map(selectedYear.Songs));
            base.OnInitialize();
        }

        private string year;

        public string Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
                NotifyOfPropertyChange(() => Year);
            }
        }

        public string ApplicationTitle
        {
            get { return AppResources.ApplicationTitle; }
        }

        private ObservableCollection<SongViewModel> songs;

        public ObservableCollection<SongViewModel> Songs
        {
            get
            {
                return songs;
            }
            set
            {
                songs = value;
                this.NotifyOfPropertyChange(() => this.Songs);
            }
        }
    }
}
