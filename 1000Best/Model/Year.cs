using _1000Best.Resources;

namespace _1000Best.Model
{
    public class Year
    {
        public string CurrentYear { get; set; }

        public string NumberOfSongs
        {
            get { return string.Format(AppResources.NumberOfSongs, Songs.Count); }
        }

        public Songs Songs { get; set; }
    }
}
