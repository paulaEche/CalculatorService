using CalculatorService.Server.Models;
using CalculatorService.Server.Services;
using CalculatorService.Server.Utils;
using CalculatorService.Server.Utils.Interfaces;

namespace CalculatorService.Test
{
    public class AddTest : IDisposable
    {
        private AddService _addService;
        private ILogging _logging;


        public AddTest()
        {
            _logging = new Logging();
            _addService = new AddService(_logging);
        }

        public void Dispose()
        {
            _logging = new Logging();
            _addService = new AddService(_logging);
        }

        [Theory]
        [InlineData(new double[2] { 1, 2 }, 3)]
        [InlineData(new double[2] { -1, 8 }, 7)]
        [InlineData(new double[2] { 1, -8 }, -7)]
        [InlineData(new double[3] { 1, 2, 4 }, 7)]
        [InlineData(new double[4] { 1, 2, -1, -2 }, 0)]
        [InlineData(new double[2] { 1.8, 2.0258 }, 3.8258)]
        [InlineData(new double[3] { -1.8, 2.0258, -3.0 }, -2.7742)]
        [InlineData(new double[2] { 0, 0 }, 0)]
        [InlineData(new double[2] { 99.99999999999999, 0.00000001 }, 100)]
        public void Add_Correctly(double[] input, double expectedOutput)
        {
            AddArguments addArg = new (input);
            AddResult? addResult = _addService.GetOperationResult(addArg) as AddResult;

            Assert.Equal(expectedOutput, addResult?.Sum);
        }

        [Theory]
        [InlineData(new double[0] { })]
        [InlineData(new double[1] { 1 })]
        public void Add_ArgumentException(double[] input)
        {
            AddArguments addArg = new (input);
            Assert.Throws<ArgumentException>(() => _addService.GetOperationResult(addArg));
        }

        [Fact]
        public void Add_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _addService.GetOperationResult(null));
        }
    }
}