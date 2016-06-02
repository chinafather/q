using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Cares.FidsII.Util
{
    public class LogUtil
    {
        private readonly static log4net.ILog logErr = log4net.LogManager.GetLogger("logger.Err");
        private readonly static log4net.ILog logInfo = log4net.LogManager.GetLogger("logger.Info");

        public static void WriteErr(string message)
        {
            logErr.Error(message);
        }

        public static void WriteInfo(string message)
        {
            logInfo.Info(message);
        }
    }
}
