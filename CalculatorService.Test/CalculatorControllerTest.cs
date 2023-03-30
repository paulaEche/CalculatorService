using CalculatorService.Server.Controllers;
using CalculatorService.Server.Models;
using CalculatorService.Server.Models.Interfaces;
using CalculatorService.Server.Services;
using CalculatorService.Server.Services.Interfaces;
using CalculatorService.Server.Utils.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CalculatorService.Test
{
    public class CalculatorControllerTest : IDisposable
    {
        private CalculatorController _calculatorController;

        public CalculatorControllerTest()
        {
            SetUpController();
        }

        private void SetUpController()
        {
            var logging = new Mock<ILogging>();
            var journalService = new Mock<IJournalService>();

            IList<IOperationService> operations = new List<IOperationService>()
            {
                new AddService(logging.Object),
                new SubtractService(logging.Object),
                new MultiplyService(logging.Object),
                new DivisionService(logging.Object),
                new SquareRootService(logging.Object)
            };

            var operationService = new Mock<IEnumerable<IOperationService>>();
            operationService.Setup(x => x.GetEnumerator()).Returns(operations.GetEnumerator());

            _calculatorController = new CalculatorController(operationService.Object, journalService.Object, logging.Object);
        }

        public void Dispose()
        {
            _calculatorController = null;
        }

        [Fact]
        public void Calculator_Controller_Add()
        {
            double[] addends = new double[4] { 1, 2, 3, 4 };
            var result = _calculatorController.Add(new AddArguments(addends));

            Assert.NotNull(result);
            Assert.IsType<ActionResult<IOperationResult>>(result);

            var value = result.Result as OkObjectResult;
            Assert.IsType<AddResult>(value.Value);
            Assert.Equal(10, (value.Value as AddResult).Sum);
        }

        [Fact]
        public void Calculator_Controller_Sub()
        {
            double minuend = 2.5;
            double subtrahend = 1.5;
            var result = _calculatorController.Sub(new SubtractArguments(minuend, subtrahend));

            Assert.NotNull(result);
            Assert.IsType<ActionResult<IOperationResult>>(result);


            var value = result.Result as OkObjectResult;
            Assert.IsType<SubtractResult>(value.Value);
            Assert.Equal(1, (value.Value as SubtractResult).Difference);
        }

        [Fact]
        public void Calculator_Controller_Mult()
        {
            double[] factors = new double[4] { 1, 2, 3, 4 };
            var result = _calculatorController.Multiply(new MultiplyArguments(factors));

            Assert.NotNull(result);
            Assert.IsType<ActionResult<IOperationResult>>(result);

            var value = result.Result as OkObjectResult;
            Assert.IsType<MultiplyResult>(value.Value);
            Assert.Equal(24, (value.Value as MultiplyResult).Product);
        }

        [Fact]
        public void Calculator_Controller_Div()
        {
            double dividend = 10;
            double divisor = 2;
            var result = _calculatorController.Division(new DivisionArguments(dividend, divisor));

            Assert.NotNull(result);
            Assert.IsType<ActionResult<IOperationResult>>(result);

            var value = result.Result as OkObjectResult;
            Assert.IsType<DivisionResult>(value.Value);
            Assert.Equal(5, (value.Value as DivisionResult).Quotient);
            Assert.Equal(0, (value.Value as DivisionResult).Remainder);
        }

        [Fact]
        public void Calculator_Controller_SqrtRoot()
        {
            double number = 25;
            var result = _calculatorController.SquareRoot(new SquareRootArguments(number));

            Assert.NotNull(result);
            Assert.IsType<ActionResult<IOperationResult>>(result);

            var value = result.Result as OkObjectResult;
            Assert.IsType<SquareRootResult>(value.Value);
            Assert.Equal(5, (value.Value as SquareRootResult).SquareRoot);
        }
    }
}