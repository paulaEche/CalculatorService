using CalculatorService.Server.Models;
using CalculatorService.Server.Models.Interfaces;
using CalculatorService.Server.Services.Interfaces;
using CalculatorService.Server.Utils.Interfaces;

namespace CalculatorService.Server.Services
{
    /// <summary>
    /// Class that handles the division operation
    /// </summary>
    public class DivisionService : IOperationService
    {
        private readonly ILogging _logging;

        public DivisionService(ILogging logging)
        {
            _logging = logging;
        }

        public IOperationResult? GetOperationResult(IOperationArguments operands)
        {
            _logging.Information("Getting Division Result");

            CheckOperands(operands);

            double dividend = ((DivisionArguments)operands).dividend;
            double divisor = ((DivisionArguments)operands).divisor;

            _logging.Information($"Dividend: {dividend} ; Divisor: {divisor}");

            if (divisor == 0d)
            {
                throw new DivideByZeroException(nameof(divisor));
            }

            try
            {
                double quotient = Math.Truncate(dividend / divisor);
                double remainder = Math.Round(Math.Abs(dividend % divisor), 4);

                return new DivisionResult(quotient, remainder);
            }
            catch (Exception ex)
            {
                _logging.Error($"The result of the division could not be obtained", ex);
            }

            return null;
        }

        private static void CheckOperands(IOperationArguments operandsArg)
        {
            if (operandsArg == null)
            {
                throw new ArgumentNullException(nameof(operandsArg));
            }

            if (operandsArg is not DivisionArguments)
            {
                throw new ArgumentException("Incorrect type of argument, it should be DivisionArguments");
            }
        }
    }
}