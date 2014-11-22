using System.Collections.Generic;
using _1000Best.ViewModels;

namespace _1000Best.Model
{
    public class Songs : List<Song>
    {
        public Songs()
        {
        }

        public Songs(IEnumerable<Song> list)
        {
            foreach (var song in list)
            {
                Add(song);
            }
        }
    }
}
