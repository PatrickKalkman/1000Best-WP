using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Caliburn.Micro;
using _1000Best.Common;
using _1000Best.Logging;
using _1000Best.Model;
using _1000Best.Resources;

namespace _1000Best.ViewModels
{
    public class ArtistViewModel : Screen, IHandle<ArtistInfoEvent>
    {
        private readonly BackgroundImageBrush backgroundImageBrush;
        private readonly SongManager songManager;
        private readonly ArtistImageUrlRetriever artistImageUrlRetriever;
        private readonly IEventAggregator eventAggregator;
        private readonly ILogging logger;

        public ArtistViewModel(BackgroundImageBrush backgroundImageBrush, 
            SongManager songManager, 
            ArtistImageUrlRetriever artistImageUrlRetriever,
            IEventAggregator eventAggregator, ILogging logger)
        {
            this.backgroundImageBrush = backgroundImageBrush;
            this.songManager = songManager;
            this.artistImageUrlRetriever = artistImageUrlRetriever;
            this.eventAggregator = eventAggregator;
            this.logger = logger;
            this.eventAggregator.Subscribe(this);
        }

        private ImageBrush backgroundImage;

        public ImageBrush BackgroundImageBrush
        {
            get
            {
                return backgroundImage;
            }
            set
            {
                backgroundImage = value;
                NotifyOfPropertyChange(() => BackgroundImageBrush);
            }
        }

        private string summary;

        public string Summary
        {
            get
            {
                return summary;
            }
            set
            {
                summary = value;
                NotifyOfPropertyChange(() => Summary);
            }
        }

        private string artistImage;

        public string ArtistImage
        {
            get
            {
                return artistImage;
            }
            set
            {
                artistImage = value;
                NotifyOfPropertyChange(() => ArtistImage);
            }
        }

        protected override void OnInitialize()
        {
            this.BackgroundImageBrush = backgroundImageBrush.GetBackground();
            SelectedArtist = songManager.GetArtist(artistName);
            Songs = new ObservableCollection<SongViewModel>(SongMapper.Map(SelectedArtist.ArtistSongs));
            artistImageUrlRetriever.GetArtistImageUrl(artistName);
            base.OnInitialize();
        }

        private string artistName;

        public string ArtistName
        {
            get
            {
                return artistName;
            }
            set
            {
                artistName = value;
                NotifyOfPropertyChange(() => ArtistName);
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

        private bool showLoadingIndicator;

        public bool ShowLoadingIndicator
        {
            get { return showLoadingIndicator; }
            set 
            { 
                showLoadingIndicator = value;
                this.NotifyOfPropertyChange(() => this.ShowLoadingIndicator);
            }
        }

        public void Handle(ArtistInfoEvent message)
        {
            if (!message.HasError)
            {
                try
                {
                    Image artistImageFromLastFm = message.Root.artist.image.Find(im => im.size.Equals("large"));
                    if (artistImageFromLastFm != null)
                    {
                        ArtistImage = artistImageFromLastFm.text;
                    }

                }
                catch (Exception error)
                {
                    ShowLoadingIndicator = false;
                    logger.Error(error.Message);
                }
            }
            else
            {
                ShowLoadingIndicator = false;
            }
        }
    }
}
 