using CalculatorService.Server.Models;
using CalculatorService.Server.Models.Interfaces;
using CalculatorService.Server.Services.Interfaces;
using CalculatorService.Server.Utils.Interfaces;

namespace CalculatorService.Server.Services
{
    /// <summary>
    /// Class that handles the operation of square root
    /// </summary>
    public class SquareRootService : IOperationService
    {
        private readonly ILogging _logging;

        public SquareRootService(ILogging logging)
        {
            _logging = logging;
        }

        public IOperationResult? GetOperationResult(IOperationArguments operands)
        {
            _logging.Information("Getting Square Root Result");

            double number = GetOperand(operands);

            _logging.Information($"Number: {number}");

            try
            {
                return new SquareRootResult(Math.Round(Math.Sqrt(number), 4));
            }
            catch (Exception ex)
            {
                _logging.Error($"The result of the square could not be obtained", ex);
            }
            return null;
        }

        private static double GetOperand(IOperationArguments operandsArg)
        {
            if (operandsArg == null)
            {
                throw new ArgumentNullException(nameof(operandsArg));
            }

            if (operandsArg is not SquareRootArguments)
            {
                throw new ArgumentException("Incorrect type of argument, it should be SquareArguments");
            }

            double number = ((SquareRootArguments)operandsArg).Number;

            if (number < 0)
            {
                throw new ArgumentException("The square root of a negative number cannot be obtained");
            }
            return number;
        }
    }
}