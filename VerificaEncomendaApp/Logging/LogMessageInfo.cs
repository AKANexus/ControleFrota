using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ControleFrota.Logging
{
    public sealed class LogMessageInfo : EventArgs
    {
        public readonly DateTime Timestamp;
        public readonly string ThreadId;
        public readonly string Level;
        public readonly string Logger;
        public readonly string Message;

        public bool IsError => Logging.Logger.ERROR.Equals(Level, StringComparison.Ordinal);
        public bool IsWarning => Logging.Logger.WARN.Equals(Level, StringComparison.Ordinal);
        public bool IsInformation => Logging.Logger.INFO.Equals(Level, StringComparison.Ordinal);
        public bool IsDebug => Logging.Logger.DEBUG.Equals(Level, StringComparison.Ordinal);

        public LogMessageInfo(string level, string logger, string message)
        {
            Timestamp = DateTime.Now;
            var thread = Thread.CurrentThread;
            ThreadId = string.IsNullOrEmpty(thread.Name) ? thread.ManagedThreadId.ToString() : thread.Name;
            Level = level;
            Logger = logger;
            Message = message;
        }

        public override string ToString()
        {
            return string.Format("{0:yyyy/MM/dd HH:mm:ss.fff} {1} {2} {3} {4}",
                Timestamp, ThreadId, Logger, Level, Message);
        }
    }

}
