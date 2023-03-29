using CalculatorService.Server.Models;
using CalculatorService.Server.Services;
using CalculatorService.Server.Utils;
using CalculatorService.Server.Utils.Interfaces;

namespace CalculatorService.Test
{
    public class SquareRootTest : IDisposable
    {
        private SquareRootService _squareService;
        private ILogging _logging;


        public SquareRootTest()
        {
            _logging = new Logging();
            _squareService = new SquareRootService(_logging);
        }

        public void Dispose()
        {
            _logging = new Logging();
            _squareService = new SquareRootService(_logging);
        }

        [Theory]
        [InlineData(25, 5)]
        [InlineData(0, 0)]
        [InlineData(6.25, 2.5)]
        public void SquareRoot_Correctly(double input, double expectedOutput)
        {
            SquareRootArguments sqrtArg = new(input);
            SquareRootResult? SquareRootResult = _squareService.GetOperationResult(sqrtArg) as SquareRootResult;

            Assert.Equal(expectedOutput, SquareRootResult?.SquareRoot);
        }

        [Fact]
        public void SquareRoot_ArgumentException()
        {
            SquareRootArguments sqrtArg = new(-25);
            Assert.Throws<ArgumentException>(() => _squareService.GetOperationResult(sqrtArg));
        }

        [Fact]
        public void SquareRoot_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _squareService.GetOperationResult(null));
        }
    }
}