using System;
using Caliburn.Micro;
using Microsoft.Phone.Tasks;
using _1000Best.Model;

namespace _1000Best.ViewModels
{
    public class SongViewModel : Screen
    {
        private readonly WebBrowserTask webBrowserTask = new WebBrowserTask();

        public SongViewModel()
        {
            this.Song = new Song {Artist = "Patrick", Title = "Toppers 2013"};
        }

        public async void PlaySpotify()
        {
            try
            {
                bool launched = await Windows.System.Launcher.LaunchUriAsync(new Uri(Song.SpotifyLaunchUri));
            }
            catch (Exception error)
            {
            }
        }

        public async void PlayNokia()
        {
            try
            {
                bool launched = await Windows.System.Launcher.LaunchUriAsync(new Uri(Song.NokiaLaunchUri));
            }
            catch (Exception error)
            {
            }
        }

        public void PlayLastFm()
        {
            try
            {
                webBrowserTask.Uri = new Uri(Song.LastFmUri, UriKind.Absolute);
                webBrowserTask.Show();
            }
            catch (Exception error)
            {
            }
        }

        public bool ShouldSpotifyBeVisible
        {
            get { return !string.IsNullOrWhiteSpace(Song.SpotifyUri); }
        }

        public bool ShouldLastFmBeVisible
        {
            get { return !string.IsNullOrWhiteSpace(Song.LastFmUri); }
        }

        public string Artist
        {
            get { return Song.Artist; }
        }

        public string Title
        {
            get { return Song.Title; }
        }

        public Song Song { get; set; }

    }
}
