using System;
using System.Threading;

namespace _1000Best.Network
{
    public class BestHttpClientResult : IAsyncResult
    {
        public string Response { get; set; }
        public string Error { get; set; }

        public bool IsCompleted { get; private set; }
        public WaitHandle AsyncWaitHandle { get; private set; }
        public object AsyncState { get; private set; }
        public bool CompletedSynchronously { get; private set; }
    }
}