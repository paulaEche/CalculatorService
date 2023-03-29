namespace CalculatorService.Server.Utils
{
    /// <summary>
    /// Class in charge of formatting the errors received
    /// </summary>
    public class ErrorsHandler
    {
        /// <summary>
        /// Method that receives the status of the error and returns it formatted
        /// </summary>
        /// <param name="status">error status</param>
        /// <returns>ErrorResponse: a class with the error code, status and message</returns>
        public static ErrorResponse GetError(int status)
        {
            var ErrorResponse = new ErrorResponse
            {
                ErrorStatus = status
            };
            switch (status / 100)
            {
                case 4:
                    ErrorResponse.ErrorCode = "InternalError";
                    ErrorResponse.ErrorMessage = "Unable to process request: ...";
                    break;

                case 5:
                    ErrorResponse.ErrorCode = "InternalError";
                    ErrorResponse.ErrorMessage = "An unexpected error condition was triggered which made impossible to fulfill the request. Please try again";
                    break;

                default:
                    ErrorResponse.ErrorCode = "UnexpectedError";
                    ErrorResponse.ErrorMessage = "An unexpected error condition was triggered which made impossible to fulfill the request. Please try again";
                    break;
            }

            return ErrorResponse;
        }
    }


    public class ErrorResponse
    {
        public ErrorResponse()
        { }

        public string? ErrorCode { get; set; }
        public int ErrorStatus { get; set; }
        public string? ErrorMessage { get; set; }
    }
}