using CalculatorService.Server.Models;
using CalculatorService.Server.Models.Interfaces;
using CalculatorService.Server.Services.Interfaces;
using CalculatorService.Server.Utils.Interfaces;

namespace CalculatorService.Server.Services
{
    /// <summary>
    /// Class that handles the subtraction operation 
    /// </summary>
    public class SubtractService : IOperationService
    {
        private readonly ILogging _logging;

        public SubtractService(ILogging logging)
        {
            _logging = logging;
        }

        public IOperationResult? GetOperationResult(IOperationArguments operands)
        {
            _logging.Information("Getting Subtract Result");

            CheckOperands(operands);

            double minuend = ((SubtractArguments)operands).minuend;
            double subtrahend = ((SubtractArguments)operands).subtrahend;

            _logging.Information($"Minuend: {minuend} ; Subtrahend: {subtrahend}");

            try
            {
                return new SubtractResult(Math.Round(minuend - subtrahend, 4));
            }
            catch (Exception ex)
            {
                _logging.Error($"The result of the subtraction could not be obtained", ex);
            }

            return null;
        }

        private static void CheckOperands(IOperationArguments operandsArg)
        {
            if (operandsArg == null)
            {
                throw new ArgumentNullException(nameof(operandsArg));
            }

            if (operandsArg is not SubtractArguments)
            {
                throw new ArgumentException("");
            }
        }
    }
}