using CalculatorService.Server.Models;
using CalculatorService.Server.Services;
using CalculatorService.Server.Utils;
using CalculatorService.Server.Utils.Interfaces;

namespace CalculatorService.Test
{
    public class DivisionTest : IDisposable
    {
        private DivisionService _divisionService;
        private ILogging _logging;

        public DivisionTest()
        {
            _logging = new Logging();
            _divisionService = new DivisionService(_logging);
        }

        public void Dispose()
        {
            _logging = new Logging();
            _divisionService = new DivisionService(_logging);
        }

        [Theory]
        [InlineData(50, 2, 25, 0)]
        [InlineData(-10, 8, -1, 2)]
        [InlineData(1.8, 2.0258, 0, 1.8)]
        [InlineData(100.001, 0.001, 100001, 0)]
        public void Division_Correctly(double dividend, double divisor, double expectedQuotientOutput, double expectedRemaindertOutput)
        {
            DivisionArguments divArg = new (dividend, divisor);
            DivisionResult? DivisionResult = _divisionService.GetOperationResult(divArg) as DivisionResult;

            Assert.Equal(expectedQuotientOutput, DivisionResult?.Quotient);
            Assert.Equal(expectedRemaindertOutput, DivisionResult?.Remainder);
        }

        [Fact]
        public void Division_Inorrectly()
        {
            Assert.Throws<ArgumentNullException>(() => _divisionService.GetOperationResult(null));
        }

        [Fact]
        public void Division_Divide_By_Zero()
        {
            DivisionArguments divArg = new (10, 0);
            Assert.Throws<DivideByZeroException>(() => _divisionService.GetOperationResult(divArg));
        }
    }
}