using static CalculatorService.Server.Utils.Interfaces.ILogging;

namespace CalculatorService.Server.Utils.Interfaces
{
    public interface ILogging
    {
        void Information(string message);
        void Error(string mensaje, Exception e);
        void Fatal(string mensaje, Exception e);
    }
}
