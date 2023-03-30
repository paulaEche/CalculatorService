using CalculatorService.Server.Utils;
using System.Globalization;
using System.Text;

namespace CalculatorService.Client
{
    public class Program
    {
        static async Task Main()
        {
            await InitializeOperation();
        }

        private static async Task InitializeOperation()
        {
            bool end = false;
            while (!end)
            {
                Console.WriteLine("What operation do you want to perform? Add, Sub, Mult, Div, SqrtR or TrackId. Write end to finish");
                string? value = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(value)) { continue; }

                if (value == "end") end = true;

                switch (value.ToUpper())
                {
                    case "ADD":
                        await Add();
                        break;
                    case "SUB":
                        await Sub();
                        break;
                    case "MULT":
                        await Mult();
                        break;
                    case "DIV":
                        await Div();
                        break;
                    case "SQRTR":
                        await SqrtR();
                        break;
                    case "TRACKID":
                        await Query();
                        break;
                    default:
                        break;
                }
            }
        }

        private static async Task GetResult(Object? jsonObject, string url)
        {
            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(jsonObject), Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add(CalculatorConstants.TrackingHeader, GetTrackIdValue().Result);
            var response = await client.PostAsync(url, jsonContent);
            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"The result is: {responseString}");
        }

        private static async Task Add()
        {
            string apiUrl = "https://localhost:7107/Add";

            var jsonObject = new
            {
                addends = GetArrayvalue("added").Result
            };

            await GetResult(jsonObject, apiUrl);
        }

        private static async Task Sub()
        {
            string apiUrl = "https://localhost:7107/Sub";
            
            var jsonObject = new {
                minuend = GetOneValue("minuend").Result,
                subtrahend = GetOneValue("subtrahend").Result
            };
            await GetResult(jsonObject, apiUrl);
        }

        private static async Task Mult()
        {
            string apiUrl = "https://localhost:7107/Mult";

            var jsonObject = new
            {
                factors = GetArrayvalue("multiplied").Result
            };
            await GetResult(jsonObject, apiUrl);
        }

        private static async Task Div()
        {
            string apiUrl = "https://localhost:7107/Div";

            var jsonObject = new
            {
                dividend = GetOneValue("dividend").Result,
                divisor = GetOneValue("divisor").Result
            };
            await GetResult(jsonObject, apiUrl);
        }

        private static async Task SqrtR()
        {
            string apiUrl = "https://localhost:7107/Sqrt";

            var jsonObject = new
            {
                number = GetOneValue("number").Result
            };
            await GetResult(jsonObject, apiUrl);
        }

        private static async Task Query()
        {
            string apiUrl = "https://localhost:7107/Query?id=";
            apiUrl += GetOneValue("trak id").Result;

            await GetResult(null, apiUrl);
        }

        private static Task<Double[]> GetArrayvalue(string operation)
        {
            var operands = new List<Double>();
            while (true)
            {
                Console.WriteLine($"Write a number (other than 0) to be {operation} and press enter to perform the operation");
                string? value = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(value))
                {
                    break;
                }
                double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double operand);

                if (operand != 0d) { operands.Add(operand); }
            }

            return Task.FromResult(operands.ToArray());
        }

        private static Task<Double> GetOneValue(string operandName)
        {
            double operand = 0;
            while (true)
            {
                Console.WriteLine($"Write a {operandName}(other than 0): ");
                string? value = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(value)) break;

                Double.TryParse(value, out operand);
                if (operand != 0d) break;
            }

            return Task.FromResult(operand);
        }

        private static Task<string?> GetTrackIdValue()
        {
            Console.WriteLine($"Write a track id value or enter to continue without it: ");
            return Task.FromResult(Console.ReadLine());

        }

    }
}