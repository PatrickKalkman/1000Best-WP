using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1000Best.Network
{
    public class BestSettings
    {
        public string LastFmBaseUriString
        {
            get
            {
                return
                    "http://ws.audioscrobbler.com/2.0/?method={0}&api_key=useyourownkey&format=json";
            }
        }
    }
}
