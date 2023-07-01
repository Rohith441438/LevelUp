using System;
using System.Collections.Generic;
using System.Text;

namespace SRP
{
    public interface ILogger
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message);
    }

    public class Logger : ILogger
    {
        public void Error(string message)
        {
            //Here is the code to log any error
        }

        public void Info(string message)
        {
            //Here is the code to log any information
        }

        public void Warn(string message)
        {
            //Here is the code to log any warning
        }
    }
}
