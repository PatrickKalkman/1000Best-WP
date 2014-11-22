using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Threading;
using Caliburn.Micro;
using Microsoft.Phone.Tasks;
using Telerik.Windows.Data;
using _1000Best.Common;
using _1000Best.Model;
using _1000Best.Resources;

namespace _1000Best.ViewModels
{
    public class MainPageViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly BackgroundImageRotator backgroundImageRotator;
        private readonly SongManager songManager;
        private readonly DispatcherTimer backgroundChangeTimer = new DispatcherTimer();

        private ImageBrush panoramaBackground;

        public MainPageViewModel()
        {
            Songs = new ObservableCollection<SongViewModel>();
            Songs.Add(new SongViewModel { Song = new Song { Artist = "Patrick", Title = "Toppers 2013" } });
            Songs.Add(new SongViewModel { Song = new Song { Artist = "Gi Joe", Title = "What is happening" } });
            Songs.Add(new SongViewModel { Song = new Song { Artist = "Patrick", Title = "Toppers 2013" } });
            Songs.Add(new SongViewModel { Song = new Song { Artist = "Patrick", Title = "Toppers 2013" } });
            Songs.Add(new SongViewModel { Song = new Song { Artist = "Patrick", Title = "Toppers 2013" } });
        }

        public MainPageViewModel(INavigationService navigationService, 
                                 BackgroundImageRotator backgroundImageRotator, 
                                 SongManager songManager)
        {
            this.navigationService = navigationService;
            this.backgroundImageRotator = backgroundImageRotator;
            this.songManager = songManager;

            StartButtonText = "start";
            AboutButtonText = "about";
            PrivacyButtonText = "privacy";
            RotatePanoramaBackground();
            InitializeAndStartTimer();

            var songDescriptor = new GenericGroupDescriptor<SongViewModel, string>(s => s.Title.Substring(0, 1).ToUpper());
            GroupedSongs = new List<GenericGroupDescriptor<SongViewModel, string>>();
            GroupedSongs.Add(songDescriptor);

            var artistDescriptor = new GenericGroupDescriptor<SongArtist, string>(a => a.Name.Substring(0, 1).ToUpper());
            GroupedArtist = new List<GenericGroupDescriptor<SongArtist, string>>();
            GroupedArtist.Add(artistDescriptor);
        }

        protected override void OnViewLoaded(object view)
        {
            FillSongs();

            ExtractFirstSongLetters();

            Artists = songManager.GetArtists();

            ExtractFirstArtistLetters();

            Years = songManager.GetYears();

            base.OnViewLoaded(view);
        }

        private void ExtractFirstArtistLetters()
        {
            FirstLetterOfArtistNames = new ObservableCollection<string>();
            List<string> firstLetterArtists = songManager.GetArtistFirstLetters();
            foreach (string firstLetterArtist in firstLetterArtists)
            {
                FirstLetterOfArtistNames.Add(firstLetterArtist);
            }
        }

        private void ExtractFirstSongLetters()
        {
            FirstLetterOfSongNames = new ObservableCollection<string>();
            List<string> firstLetters = songManager.GetSongFirstLetters();
            foreach (string firstLetter in firstLetters)
            {
                FirstLetterOfSongNames.Add(firstLetter);
            }
        }

        private void FillSongs()
        {
            Songs = new ObservableCollection<SongViewModel>();
            List<SongViewModel> songs = SongMapper.Map(songManager.Load());
            foreach (SongViewModel songViewModel in songs)
            {
                Songs.Add(songViewModel);
            }
        }

        private void InitializeAndStartTimer()
        {
            this.backgroundChangeTimer.Interval = TimeSpan.FromSeconds(10);
            this.backgroundChangeTimer.Tick += BackgroundChangeTimer_Tick;
            this.backgroundChangeTimer.Start();
        }

        private void BackgroundChangeTimer_Tick(object sender, EventArgs e)
        {
            RotatePanoramaBackground();
        }

        public string ApplicationName
        {
            get { return AppResources.ApplicationTitle;  }
        }

        public string PageTitle
        {
            get { return AppResources.MainPageTitle; }
        }

        public void Privacy()
        {
            var uri = navigationService.UriFor<PrivacyViewModel>().BuildUri();
            navigationService.Navigate(uri);
        }

        public void SubmitReview()
        {
            new MarketplaceReviewTask().Show(); 
        }

        public void MoreApps()
        {
            var searchTask = new MarketplaceSearchTask();
            searchTask.SearchTerms = "Patrick Kalkman";
            searchTask.Show();           
        }

        private Years years;

        public Years Years
        {
            get { return years; }
            set
            {
                years = value;
                NotifyOfPropertyChange(() => Years);
            }
        }

        private Artists artists;

        public Artists Artists
        { 
            get { return artists; }
            set
            {
                artists = value;
                NotifyOfPropertyChange(() => Artists);
            }
        }

        private SongArtist selectedArtist;

        public SongArtist SelectedArtist
        {
            get { return selectedArtist; }
            set 
            { 
                selectedArtist = value;
                NotifyOfPropertyChange(() => SelectedArtist);
                navigationService.UriFor<ArtistViewModel>().WithParam(vm => vm.ArtistName, selectedArtist.Name).Navigate();
            }
        }

        private Year selectedYear;

        public Year SelectedYear
        {
            get { return selectedYear; }
            set
            {
                selectedYear = value;
                NotifyOfPropertyChange(() => SelectedYear);
                navigationService.UriFor<YearViewModel>().WithParam(vm => vm.Year, selectedYear.CurrentYear).Navigate();
            }
        }

        private List<GenericGroupDescriptor<SongViewModel, string>> groupedSongs;

        public List<GenericGroupDescriptor<SongViewModel, string>> GroupedSongs
        {
            get { return groupedSongs; }
            set
            {
                groupedSongs = value;
                NotifyOfPropertyChange(() => GroupedSongs);
            }
        }

        private List<GenericGroupDescriptor<SongArtist, string>> groupedArtist;

        public List<GenericGroupDescriptor<SongArtist, string>> GroupedArtist
        {
            get { return groupedArtist; }
            set
            {
                groupedArtist = value;
                NotifyOfPropertyChange(() => GroupedArtist);
            }
        }
        
        private ObservableCollection<string> firstLetterOfSongNames;

        public ObservableCollection<string> FirstLetterOfSongNames
        {
            get { return firstLetterOfSongNames; }
            set
            {
                firstLetterOfSongNames = value;
                NotifyOfPropertyChange(() => FirstLetterOfSongNames);
            }
        }

        private ObservableCollection<string> firstLetterOfArtistNames;

        public ObservableCollection<string> FirstLetterOfArtistNames
        {
            get { return firstLetterOfArtistNames; }
            set 
            { 
                firstLetterOfArtistNames = value;
                NotifyOfPropertyChange(() => FirstLetterOfArtistNames);
            }
        }

        private ObservableCollection<SongViewModel> songs;

        public ObservableCollection<SongViewModel> Songs
        {
            get { return songs; }
            set 
            { 
                songs = value;
                NotifyOfPropertyChange(() => Songs);
            }
        }

        public ImageBrush PanoramaBackground
        {
            get
            {
                return panoramaBackground;
            }

            set
            {
                panoramaBackground = value;
                NotifyOfPropertyChange(() => PanoramaBackground);
            }
        }

        private void RotatePanoramaBackground()
        {
            PanoramaBackground = backgroundImageRotator.Rotate();
        }

        public void About()
        {
            navigationService.Navigate(new Uri("/YourLastAboutDialog;component/AboutPage.xaml", UriKind.Relative));
        }

        private string aboutButtonText;

        public string AboutButtonText
        {
            get { return aboutButtonText; }
            set
            {
                aboutButtonText = value;
                NotifyOfPropertyChange(() => AboutButtonText);
            }
        }

        private string privacyButtonText;

        public string PrivacyButtonText
        {
            get { return privacyButtonText; }
            set
            {
                privacyButtonText = value;
                NotifyOfPropertyChange(() => PrivacyButtonText);
            }
        }

        private string startButtonText;

        public string StartButtonText
        {
            get { return startButtonText; }
            set
            {
                startButtonText = value;
                NotifyOfPropertyChange(() => StartButtonText);
            }
        }

        private Uri icon;

        public Uri Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                NotifyOfPropertyChange(() => Icon);
            }
        }
    }
}