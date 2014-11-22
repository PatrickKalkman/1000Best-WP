using System;

namespace _1000Best.Logging
{
    public interface ILogging
    {
        void Error(string message, Exception error);

        void Error(string message);
    }
}