using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1000Best.Model
{
    public class Song
    {
        public string Artist { get; set; }
        
        public string Title { get; set; }
        
        public string Year { get; set; }
        
        public string SpotifyUri { get; set; }

        public string SpotifyLaunchUri
        {
            get { return SpotifyUri.Replace("http://open.spotify.com/track/", "spotify:track:"); }
        }

        public string LastFmUri { get; set; }

        public string NokiaLaunchUri
        {
            get { return string.Format("nokia-music://search/anything/?term={0} {1}", Artist, Title); }
        }
    } 
}
