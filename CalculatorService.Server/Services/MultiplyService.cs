using CalculatorService.Server.Models;
using CalculatorService.Server.Models.Interfaces;
using CalculatorService.Server.Services.Interfaces;
using CalculatorService.Server.Utils.Interfaces;

namespace CalculatorService.Server.Services
{
    /// <summary>
    /// Class that handles the multiplying operation
    /// </summary>
    public class MultiplyService : IOperationService
    {
        private readonly ILogging _logging;

        public MultiplyService(ILogging logging)
        {
            _logging = logging;
        }

        public IOperationResult? GetOperationResult(IOperationArguments operands)
        {
            _logging.Information("Getting Multiply Result");

            double[] factors = GetFactors(operands);

            _logging.Information($"Factors: {factors}");

            try
            {
                return new MultiplyResult(Math.Round(factors.Aggregate((x, y) => x * y), 4));
            }
            catch (Exception ex)
            {
                _logging.Error($"The result of the multiplication  could not be obtained", ex);
            }
            return null;
        }

        private static double[] GetFactors(IOperationArguments operands)
        {
            if (operands == null)
            {
                throw new ArgumentNullException(nameof(operands));
            }

            if (operands is not MultiplyArguments)
            {
                throw new ArgumentException("Incorrect type of argument, it should be MultiplyArguments");
            }

            double[] factors = ((MultiplyArguments)operands).Factors;

            if (factors.Length < 2)
            {
                throw new ArgumentException("Not enough arguments");
            }

            return factors;
        }
    }
}