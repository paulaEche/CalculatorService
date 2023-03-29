namespace CalculatorService.Server.Utils
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Method that retrieves the requested header
        /// </summary>
        /// <param name="request"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static string GetHeader(this HttpRequest request, string header)
        {
            return request.Headers.FirstOrDefault(h => h.Key.ToUpper() == header.ToUpper()).Value;
        }
    }
}