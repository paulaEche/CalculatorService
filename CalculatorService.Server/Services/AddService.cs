using CalculatorService.Server.Models;
using CalculatorService.Server.Models.Interfaces;
using CalculatorService.Server.Services.Interfaces;
using CalculatorService.Server.Utils.Interfaces;

namespace CalculatorService.Server.Services
{
    /// <summary>
    /// Class that handles the addition operation
    /// </summary>
    public class AddService : IOperationService
    {
        private readonly ILogging _logging;

        public AddService(ILogging logging)
        {
            _logging = logging;
        }

        public IOperationResult? GetOperationResult(IOperationArguments operands)
        {
            _logging.Information("Getting Add Result");

            double[] addends = GetAddenders(operands);

            _logging.Information($"Addends: {addends}");

            try
            {
                return new AddResult(Math.Round(addends.Sum(), 4));
            }
            catch (Exception ex)
            {
                _logging.Error($"The result of the sum could not be obtained", ex);
            }
            return null;
        }

        private static double[] GetAddenders(IOperationArguments operands)
        {
            if (operands == null)
            {
                throw new ArgumentNullException(nameof(operands));
            }

            if (operands is not AddArguments)
            {
                throw new ArgumentException("Incorrect type of argument, it should be AddArguments");
            }

            double[] addends = ((AddArguments)operands).Addends;

            if (addends.Length < 2)
            {
                throw new ArgumentException("Not enough arguments");
            }

            return addends;
        }
    }
}