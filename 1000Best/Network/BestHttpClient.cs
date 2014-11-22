using System;
using System.IO;
using System.Net;

namespace _1000Best.Network
{
    public class BestHttpClient
    {
        private readonly BestSettings bestSettings;
        private HttpWebRequest httpRequest;

        public BestHttpClient(BestSettings bestSettings)
        {
            this.bestSettings = bestSettings;
        }

        public void GetResponse(string method, AsyncCallback methodToCall)
        {
            var requestUri = new Uri(string.Format(bestSettings.LastFmBaseUriString, method));
            httpRequest = (HttpWebRequest)HttpWebRequest.Create(requestUri);
            httpRequest.BeginGetResponse(req =>
            {
                var httpWebRequest = (HttpWebRequest)req.AsyncState;
                try
                {
                    using (var webResponse = httpWebRequest.EndGetResponse(req))
                    {
                        using (var reader = new StreamReader(webResponse.GetResponseStream()))
                        {
                            string response = reader.ReadToEnd();
                            methodToCall(new BestHttpClientResult { Response = response });
                        }
                    }
                }
                catch (Exception e)
                {
                    methodToCall(new BestHttpClientResult() { Error = e.Message });
                }

            }, httpRequest);
        }
    }
}
