using CalculatorService.Server.Models;
using CalculatorService.Server.Models.Interfaces;

namespace CalculatorService.Server.Utils
{
    /// <summary>
    /// Class to return the string of the requested operation
    /// </summary>
    public static class OperationFormatter
    {
        private static string AddCalculationStr(double[] addends, double result)
        {
            return $"{string.Join(" + ", addends)} = {result}";
        }
        private static string SubCalculationStr(double minuend, double subtrahend, double result)
        {
            return $"{minuend} - {subtrahend} = {result}";
        }
        private static string MultCalculationStr(double[] factors, double result)
        {
            return $"{string.Join(" * ", factors)} = {result}";
        }
        private static string DivCalculationStr(double dividend, double divisor, double quotient, double remainder)
        {
            return $"{dividend} / {divisor} = {quotient} AND remainder: {remainder}";
        }
        private static string SqrtCalculationStr(double number, double result)
        {
            return $"x^{number} = {result}";
        }

        /// <summary>
        /// Method that checks the operation we are performing and calls the corresponding method in each case. 
        /// </summary>
        /// <param name="operands">Operation operands</param>
        /// <param name="result">Operation result</param>
        /// <returns>Operation string</returns>
        public static string OperationString(IOperationArguments operands, IOperationResult result)
        {
            return operands switch
            {
                AddArguments => AddCalculationStr(((AddArguments)operands).Addends, ((AddResult)result).Sum),

                SubtractArguments => SubCalculationStr(((SubtractArguments)operands).Minuend, ((SubtractArguments)operands).Subtrahend,
                                        ((SubtractResult)result).Difference),

                MultiplyArguments => MultCalculationStr(((MultiplyArguments)operands).Factors, ((MultiplyResult)result).Product),

                DivisionArguments => DivCalculationStr(((DivisionArguments)operands).Dividend, ((DivisionArguments)operands).Divisor,
                                        ((DivisionResult)result).Quotient, ((DivisionResult)result).Remainder),

                SquareRootArguments => SqrtCalculationStr(((SquareRootArguments)operands).Number, ((SquareRootResult)result).SquareRoot),

                _ => String.Empty,
            };
        }
    }
}