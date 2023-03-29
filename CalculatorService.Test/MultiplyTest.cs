using CalculatorService.Server.Models;
using CalculatorService.Server.Services;
using CalculatorService.Server.Utils;
using CalculatorService.Server.Utils.Interfaces;

namespace CalculatorService.Test
{
    public class MultiplyTest : IDisposable
    {
        private MultiplyService _multiplyService;
        private ILogging _logging;


        public MultiplyTest()
        {
            _logging = new Logging();
            _multiplyService = new MultiplyService(_logging);
        }

        public void Dispose()
        {
            _logging = new Logging();
            _multiplyService = new MultiplyService(_logging);
        }

        [Theory]
        [InlineData(new double[2] { 10, 2 }, 20)]
        [InlineData(new double[2] { -1, 8 }, -8)]
        [InlineData(new double[2] { 0, 8 }, 0)]
        [InlineData(new double[3] { 10, 2, 4 }, 80)]
        [InlineData(new double[4] { 3, 2, -1, -8 }, 48)]
        [InlineData(new double[2] { 1.8, 2.0258 }, 3.6464)]
        [InlineData(new double[3] { -1.8, 2.0258, -3.0 }, 10.9393)]
        [InlineData(new double[2] { 0, 0 }, 0)]
        [InlineData(new double[2] { 99.99999999999999, 0.00000001 }, 0)]
        public void Multiply_Correctly(double[] input, double expectedOutput)
        {
            MultiplyArguments multArg = new (input);
            MultiplyResult? MultiplyResult = _multiplyService.GetOperationResult(multArg) as MultiplyResult;

            Assert.Equal(expectedOutput, MultiplyResult?.Product);
        }

        [Theory]
        [InlineData(new double[0] { })]
        [InlineData(new double[1] { 1 })]
        public void Multiply_ArgumentException(double[] input)
        {
            MultiplyArguments addArg = new (input);
            Assert.Throws<ArgumentException>(() => _multiplyService.GetOperationResult(addArg));
        }

        [Fact]
        public void Multiply_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _multiplyService.GetOperationResult(null));
        }
    }
}