using CalculatorService.Server.Models;
using CalculatorService.Server.Services;
using CalculatorService.Server.Utils;
using CalculatorService.Server.Utils.Interfaces;

namespace CalculatorService.Test
{
    public class SubtractTest : IDisposable
    {
        private SubtractService _subtractService;
        private ILogging _logging;


        public SubtractTest()
        {
            _logging = new Logging();
            _subtractService = new SubtractService(_logging);
        }

        public void Dispose()
        {
            _logging = new Logging();
            _subtractService = new SubtractService(_logging);
        }

        [Theory]
        [InlineData(5, 2, 3)]
        [InlineData(-1, 8, -9)]
        [InlineData(1, -8, 9)]
        [InlineData(1.8, 2.0258, -0.2258)]
        [InlineData(0, 0, 0)]
        [InlineData(100.001, 0.001, 100)]
        public void Subtract_Correctly(double minuend, double subtrahend, double expectedOutput)
        {
            SubtractArguments subArg = new(minuend, subtrahend);
            SubtractResult? SubtractResult = _subtractService.GetOperationResult(subArg) as SubtractResult;

            Assert.Equal(expectedOutput, SubtractResult?.Difference);
        }

        [Fact]
        public void Subtract_Inorrectly()
        {
            Assert.Throws<ArgumentNullException>(() => _subtractService.GetOperationResult(null));
        }
    }
}