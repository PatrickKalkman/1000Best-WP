using System;
using Caliburn.Micro;
using Newtonsoft.Json;
using _1000Best.Logging;
using _1000Best.Model;
using _1000Best.Network;

namespace _1000Best.Model
{
    public class ArtistImageUrlRetriever
    {
        private readonly BestHttpClient bestHttpClient;
        private readonly IEventAggregator eventAggregator;
        private readonly ILogging logger;

        public ArtistImageUrlRetriever(BestHttpClient bestHttpClient, IEventAggregator eventAggregator, ILogging logger)
        {
            this.bestHttpClient = bestHttpClient;
            this.eventAggregator = eventAggregator;
            this.logger = logger;
        }

        public void GetArtistImageUrl(string artistName)
        {
            try
            {
                //http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=Cher&api_key=useyourownkey&format=json
                string getArtistInfoMethod = "artist.getinfo&artist=" + artistName;
                bestHttpClient.GetResponse(getArtistInfoMethod,
                    r =>
                    {
                        try
                        {
                            var result = (BestHttpClientResult)r;
                            var root = JsonConvert.DeserializeObject<RootObject>(result.Response);
                            eventAggregator.Publish(new ArtistInfoEvent {Root = root});
                        }
                        catch (Exception networkError)
                        {
                            eventAggregator.Publish(new ArtistInfoEvent {Root = null, HasError = true});
                            logger.Error(networkError.Message);
                        }
                    });
            }
            catch (Exception error)
            {
                logger.Error(error.Message);
            }
        }
    }
}
