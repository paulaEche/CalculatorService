using CalculatorService.Server.Services.Interfaces;
using CalculatorService.Server.Utils;
using CalculatorService.Server.Utils.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.Server.Controllers
{
    /// <summary>
    /// Journal Controller that contains the Journal API Rest endpoints
    /// </summary>
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;
        private readonly ILogging _logging;

        public JournalController(IJournalService journalService, ILogging logging)
        {
            _journalService = journalService;
            _logging = logging;
        }

        /// <summary>
        /// Metodo that returns history of requested operations since the last application restart
        /// </summary>
        /// <param name="id">tracking id</param>
        /// <returns>list of operations</returns>
        [HttpPost("Query")]
        public ActionResult GetQuery(string id)
        {
            if (!ModelState.IsValid)
            {
                var error = ErrorsHandler.GetError(StatusCodes.Status400BadRequest);
                return StatusCode(error.ErrorStatus, error);
            }
            try
            {
                var journalResults = _journalService.GetJournalItemsById(id);
                return Ok(journalResults);
            }
            catch (Exception e)
            {
                _logging.Error($"Error getting the journal items", e);
            }

            var internalError = ErrorsHandler.GetError(StatusCodes.Status500InternalServerError);
            return StatusCode(internalError.ErrorStatus, internalError);
        }
    }
}