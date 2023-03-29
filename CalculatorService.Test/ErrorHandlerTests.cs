using CalculatorService.Server.Utils;

namespace CalculatorService.Test
{
    public class ErrorHandlerTests
    {
        private const string ErrorCode4XX = "InternalError";
        private const string ErrorCode5XX = "InternalError";
        private const string ErrorCodeDefault = "UnexpectedError";

        private const string ErrorMessage4XX = "Unable to process request: ...";
        private const string ErrorMessage5XX = "An unexpected error condition was triggered which made impossible to fulfill the request. Please try again";
        private const string ErrorMessageDefault = "An unexpected error condition was triggered which made impossible to fulfill the request. Please try again";



        [Fact]
        public void Error_400Code()
        {
            var random = new Random();
            int code = random.Next(400, 499);
            ErrorResponse error = ErrorsHandler.GetError(code);
            Assert.NotNull(error);
            Assert.Equal(error.ErrorStatus, code);
            Assert.Equal(error.ErrorCode, ErrorCode4XX);
            Assert.Equal(error.ErrorMessage, ErrorMessage4XX);
        }

        [Fact]
        public void Error_500Code()
        {
            var random = new Random();
            int code = random.Next(500, 599);
            ErrorResponse error = ErrorsHandler.GetError(code);
            Assert.NotNull(error);
            Assert.Equal(error.ErrorStatus, code);
            Assert.Equal(error.ErrorCode, ErrorCode5XX);
            Assert.Equal(error.ErrorMessage, ErrorMessage5XX);
        }

        [Fact]
        public void Error_DefaultCode()
        {
            var random = new Random();
            int codeHigh = random.Next(600, 999);
            int codeLow = random.Next(1, 399);
            ErrorResponse error = ErrorsHandler.GetError(codeHigh);
            Assert.NotNull(error);
            Assert.Equal(error.ErrorStatus, codeHigh);
            Assert.Equal(error.ErrorCode, ErrorCodeDefault);
            Assert.Equal(error.ErrorMessage, ErrorMessageDefault);
            error = ErrorsHandler.GetError(codeLow);
            Assert.NotNull(error);
            Assert.Equal(error.ErrorStatus, codeLow);
            Assert.Equal(error.ErrorCode, ErrorCodeDefault);
            Assert.Equal(error.ErrorMessage, ErrorMessageDefault);
        }
    }
}