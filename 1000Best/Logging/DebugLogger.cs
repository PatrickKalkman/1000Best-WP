using System;
using System.Diagnostics;

namespace _1000Best.Logging
{
    public class DebugLogger : ILogging
    {
        public void Error(string message, Exception error)
        {
            Debug.WriteLine(message);
        }

        public void Error(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
