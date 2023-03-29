using CalculatorService.Server.Utils.Interfaces;
using Serilog;

namespace CalculatorService.Server.Utils
{
    /// <summary>
    /// A logging class, in this case on Serilog, where we indicate how to save information or errors in the log.
    /// This way, if one day we want to change the library to store the logs, we only have to change it here. 
    /// </summary>
    public class Logging : ILogging
    {
        public void Information(string message)
        {
            Log.Information(message);
        }

        public void Error(string message, Exception e)
        {
            Log.Error(e, message);
        }

        public void Fatal(string message, Exception e)
        {
            Log.Fatal(e, message);
        }
    }
}