using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingManagement
{
    public interface ILogService
    {
        //void LogError(string logmessage);
        void LogException(Exception exception);
        void LogMessage(string message);
    }
}
