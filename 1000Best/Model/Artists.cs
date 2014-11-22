using System.Collections.Generic;

namespace _1000Best.Model
{
    public class Artists : List<SongArtist>
    {
        public Artists()
        {
        }

        public Artists(IEnumerable<SongArtist> source) : base(source)
        {
        }
    }
}
