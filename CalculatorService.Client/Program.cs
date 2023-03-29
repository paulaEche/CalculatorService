using System.Globalization;

class Program
{
    static async Task Main(string[] args)
    {
       // await Resta().ConfigureAwait(false);
        await Suma().ConfigureAwait(false);
    }

    private static async Task Suma()
    {
        string apiUrl = "https://localhost:7107/Add";
        double param1 = 5;
        double param2 = 3;


        double[] doubles = new double[] { param1, param2 };
        string[] stringNumbers = Array.ConvertAll(doubles, n => n.ToString(CultureInfo.InvariantCulture));
        string numbers = string.Join(",", stringNumbers);
        //string url = ($"{apiUrl}?addends=1&addends=2&addends=3&addends=4"); 
        string url = apiUrl + "?addends={1,2,3}";

        using (var client = new HttpClient())
        {
            var response = await client.PostAsync(url, null);
            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"El resultado de la llamada es: {responseString}");
        }
    }

    private static async Task Resta()
    {
        string apiUrl = "https://localhost:7107/Sub";
        double param1 = 5.6;
        double param2 = 3.2;

        string url = $"{apiUrl}?minuend={param1}&subtrahend={param2}";
        url = url.Replace(",", ".");

        using (var client = new HttpClient())
        {
            var response = await client.PostAsync(url, null);
            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"El resultado de la llamada es: {responseString}");
        }
    }


}