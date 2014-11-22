using System.Collections.Generic;

namespace _1000Best.Model
{
    public class Years : List<Year>
    {
        public Years()
        {
        }

        public Years(IEnumerable<Year> source) : base(source)
        {
        }
    }
}