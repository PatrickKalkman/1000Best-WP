using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Resources;
using Newtonsoft.Json;

namespace _1000Best.Model
{
    public class SongManager
    {
        private Songs songs;

        private Artists artists;

        private Years years;

        public Songs Load()
        {
            ReadSongs();
            return songs;
        }

        public Artists GetArtists()
        {
            ReadSongs();
            ExtractArtists();
            return artists;
        }

        public Years GetYears()
        {
            ReadSongs();
            ExtractYears();
            return years;
        }

        private void ExtractYears()
        {
            var songYears = new Dictionary<string, Songs>();
            foreach (Song song in songs)
            {
                if (!songYears.ContainsKey(song.Year))
                {
                    songYears[song.Year] = new Songs();
                }
                songYears[song.Year].Add(song);
            }

            years = new Years();
            foreach (KeyValuePair<string, Songs> keyValuePair in songYears)
            {
                years.Add(new Year() { CurrentYear = keyValuePair.Key, Songs = keyValuePair.Value});
            }
            years = new Years(years.OrderBy(x => x.CurrentYear));
        }

        private void ReadSongs()
        {
            if (songs == null)
            {
                var allSongs = JsonConvert.DeserializeObject<Songs>(ReadMessagesJSON());
                songs = new Songs(allSongs.OrderBy(x => x.Title));
            }
        }

        private void ExtractArtists()
        {
            var artistSongs = new Dictionary<string, Songs>();

            foreach (Song song in songs)
            {
                if (!artistSongs.ContainsKey(song.Artist))
                {
                    artistSongs[song.Artist] = new Songs();
                }

                artistSongs[song.Artist].Add(song);
            }

            artists = new Artists();
            foreach (KeyValuePair<string, Songs> keyValuePair in artistSongs)
            {
                artists.Add(new SongArtist { Name = keyValuePair.Key, ArtistSongs = keyValuePair.Value });
            }
            artists = new Artists(artists.OrderBy(x => x.Name));
        }

        public string ReadMessagesJSON()
        {
            StreamResourceInfo streamResourceInfo = Application.GetResourceStream(new Uri("Assets/1000 Songs.json", UriKind.Relative));

            if (streamResourceInfo != null)
            {
                using (Stream resourceStream = streamResourceInfo.Stream)
                {
                    if (resourceStream.CanRead)
                    {
                        using (var streamReader = new StreamReader(resourceStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
            throw new InvalidOperationException("Cannot find or read the message file.");
        }

        public SongArtist GetArtist(string artistName)
        {
            if (!string.IsNullOrEmpty(artistName))
            {
                if (artists != null)
                {
                    return artists.Single(a => a.Name == artistName);
                }

                throw new InvalidOperationException("Artists collection is null and therefore cannot be searched.");
            }

            throw new ArgumentException("Cannot search a null or empty artist string.");
        }

        public Year GetYear(string year)
        {
            if (!string.IsNullOrEmpty(year))
            {
                if (years != null)
                {
                    return years.Single(a => a.CurrentYear == year);
                }

                throw new InvalidOperationException("Years collection is null and therefore cannot be searched.");
            }

            throw new ArgumentException("Cannot search a null or empty year string.");
        }

        public List<string> GetSongFirstLetters()
        {
            var firstLetters = new List<string>();
            if (songs != null)
            {
                foreach (Song song in songs)
                {
                    string firstLetter = song.Title.Substring(0, 1);
                    if (!firstLetters.Exists(s => s == firstLetter))
                    {
                        firstLetters.Add(firstLetter);
                    }
                }
            }
            return firstLetters;
        }

        public List<string> GetArtistFirstLetters()
        {
            var firstArtistLetters = new List<string>();
            if (artists != null)
            {
                foreach (SongArtist artist in artists)
                {
                    string firstLetter = artist.Name.Substring(0, 1);
                    if (!firstArtistLetters.Exists(s => s == firstLetter))
                    {
                        firstArtistLetters.Add(firstLetter);
                    }
                }
            }
            return firstArtistLetters;
        }
    }
}