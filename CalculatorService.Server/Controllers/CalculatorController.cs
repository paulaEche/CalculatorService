using CalculatorService.Server.Models;
using CalculatorService.Server.Models.Interfaces;
using CalculatorService.Server.Services;
using CalculatorService.Server.Services.Interfaces;
using CalculatorService.Server.Utils;
using CalculatorService.Server.Utils.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.Server.Controllers
{
    /// <summary>
    /// Calculator Controller that contains the Calculator API Rest endpoints.
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly IEnumerable<IOperationService> _operationService;
        private readonly IJournalService _journalService;
        private readonly ILogging _logging;

        public CalculatorController(IEnumerable<IOperationService> operationService,
            IJournalService journalService, ILogging logging)
        {
            _operationService = operationService;
            _journalService = journalService;
            _logging = logging;
        }

        /// <summary>
        /// Generic method for run the different operations
        /// </summary>
        /// <typeparam name="T">T of Type IOperationArguments: the different types of arguments for each operation</typeparam>
        /// <param name="operands">operands on which the operation is to be run</param>
        /// <returns></returns>
        public ActionResult<IOperationResult> GetOperationResult<T>(IOperationArguments operands)
            where T : class, IOperationService
        {
            if (!ModelState.IsValid)
            {
                var error = ErrorsHandler.GetError(StatusCodes.Status400BadRequest);
                return StatusCode(error.ErrorStatus, error);
            }

            try
            {
                var result = _operationService
                    .First(x => x.GetType() == typeof(T))
                    .GetOperationResult(operands);

                if (result != null)
                {
                    SaveOperation(Request.GetHeader(CalculatorConstants.TrackingHeader), operands, result);
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                _logging.Error($"Error getting the requested operation: {_operationService.GetType()}", e);
            }

            var internalError = ErrorsHandler.GetError(StatusCodes.Status500InternalServerError);
            return StatusCode(internalError.ErrorStatus, internalError);
        }

        /// <summary>
        /// Method that checks if the header with the track id has been recived and stores the operation in memory.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="operands"></param>
        /// <param name="result"></param>
        private void SaveOperation(string trackId, IOperationArguments operands, IOperationResult result)
        {
            if (string.IsNullOrWhiteSpace(trackId)) return;

            string calculation = OperationFormatter.OperationString(operands, result);
            _journalService.Save(trackId, operands.GetType().Name.Replace("Arguments", ""), calculation, DateTime.Now);

        }

        /// <summary>
        /// Method that receives an array of doubles to return it adds up, if the track id header is passed, stores the operation in memory
        /// </summary>
        /// <param name="addends">array of numbers to be added</param>
        /// <returns>Response Object with the result, value of the sum requested or an error</returns>
        [HttpPost("Add")]
        public ActionResult<IOperationResult> Add(double[] addends)
        {
            AddArguments addArg = new(addends);
            return GetOperationResult<AddService>(addArg);
        }

        /// <summary>
        /// Method that receives two values to return its subtraction, if the track id header is passed, stores the operation in memory
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subtrahend"></param>
        /// <returns>Response Object with the result, value of the subtract requested or an error</returns>
        [HttpPost("Sub")]
        public ActionResult<IOperationResult> Sub(double minuend, double subtrahend)
        {
            SubtractArguments subArg = new(minuend, subtrahend);
            return GetOperationResult<SubtractService>(subArg);
        }

        /// <summary>
        /// Method that receives an array of doubles to return it multiplying, if the track id header is passed, stores the operation in memory
        /// </summary>
        /// <param name="factors">array of numbers to be multiplied</param>
        /// <returns>Response Object with the result, value of the multiply requested or an error</returns>
        [HttpPost("Mult")]
        public ActionResult<IOperationResult> Multiply(double[] factors)
        {
            MultiplyArguments multArg = new(factors);
            return GetOperationResult<MultiplyService>(multArg);

        }

        /// <summary>
        /// Method that receives two values to return its division, if the track id header is passed, stores the operation in memory
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <returns>Response Object with the result, value of the division requested or an error</returns>
        [HttpPost("Div")]
        public ActionResult<IOperationResult> Division(double dividend, double divisor)
        {
            DivisionArguments divArg = new(dividend, divisor);
            return GetOperationResult<DivisionService>(divArg);

        }

        /// <summary>
        /// Method that receives a value to return its square root, if the track id header is passed, stores the operation in memory
        /// </summary>
        /// <param name="number">Number from which you want to extract the root</param>
        /// <returns>Response Object with the result, value of the square root requested or an error</returns>
        [HttpPost("Sqrt")]
        public ActionResult<IOperationResult> SquareRoot(double number)
        {
            SquareRootArguments squareArg = new(number);
            return GetOperationResult<SquareRootService>(squareArg);
        }
    }
}