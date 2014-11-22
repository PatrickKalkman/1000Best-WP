using _1000Best.Resources;

namespace _1000Best.Model
{
    public class SongArtist
    {
        public string Name { get; set; }

        public string NumberOfSongs
        {
            get
            {
                if (ArtistSongs.Count > 1)
                {
                    return string.Format(AppResources.NumberOfSongs, ArtistSongs.Count);
                }
                return string.Format(AppResources.SingleSong, ArtistSongs.Count);
            }
        }

        public Songs ArtistSongs { get; set; }
    }
}
