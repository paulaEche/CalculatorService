using CalculatorService.Server.Models;
using CalculatorService.Server.Utils;

namespace CalculatorService.Test
{
    public class OperationFormatterTest
    {
        [Fact]
        public void Add_Calculation_Formated()
        {
            AddArguments arguments = new(new double[3] { 1.5, 2, 3 });
            AddResult result = new(6.5);
            string expectedValue = "1,5 + 2 + 3 = 6,5";

            var value = OperationFormatter.OperationString(arguments, result);
            Assert.Equal(expectedValue, value);
        }

        [Fact]
        public void Sub_Calculation_Formated()
        {
            SubtractArguments arguments = new(5.9, 0.9);
            SubtractResult result = new(5);
            string expectedValue = "5,9 - 0,9 = 5";

            var value = OperationFormatter.OperationString(arguments, result);
            Assert.Equal(expectedValue, value);
        }

        [Fact]
        public void Mult_Calculation_Formated()
        {
            MultiplyArguments arguments = new(new double[3] { 1.5, 2, 3 });
            MultiplyResult result = new(3.45);
            string expectedValue = "1,5 * 2 * 3 = 3,45";

            var value = OperationFormatter.OperationString(arguments, result);
            Assert.Equal(expectedValue, value);
        }

        [Fact]
        public void Div_Calculation_Formated()
        {
            DivisionArguments arguments = new(10, 2.5);
            DivisionResult result = new(4, 0);
            string expectedValue = "10 / 2,5 = 4 AND remainder: 0";

            var value = OperationFormatter.OperationString(arguments, result);
            Assert.Equal(expectedValue, value);
        }
    }
}