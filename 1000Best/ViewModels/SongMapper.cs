using System.Collections.Generic;
using _1000Best.Model;

namespace _1000Best.ViewModels
{
    public class SongMapper
    {
        public static List<SongViewModel> Map(Songs songsToMap)
        {
            var songViewModels = new List<SongViewModel>();

            foreach (Song songToMap in songsToMap)
            {
                songViewModels.Add(new SongViewModel {Song = songToMap});
            }

            return songViewModels;
        }
    }
}