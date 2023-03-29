namespace CalculatorService.Server.Models
{
    public class JournalItem
    {
        public JournalItem(string id, string operation, string calculation, DateTime date)
        {
            Id = id;
            Operation = operation;
            Calculation = calculation;
            Date = date;
        }

        public string Id { get; }
        public string Operation { get; }
        public string Calculation { get; }
        public DateTime Date { get; }
    }
}