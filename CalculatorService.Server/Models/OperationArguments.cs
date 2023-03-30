using CalculatorService.Server.Models.Interfaces;

namespace CalculatorService.Server.Models
{
    public class AddArguments : IOperationArguments
    {
        public  double[] Addends { get; set; } = Array.Empty<double>();

        public AddArguments() { }

        public AddArguments(double[] addends)
        {
            this.Addends = addends;
        }
    }

    public class SubtractArguments : IOperationArguments
    {
        public double Minuend { get; set; }
        public  double Subtrahend { get; set; }

        public SubtractArguments() { }

        public SubtractArguments(double minuend, double subtrahend)
        {
            Minuend = minuend;
            Subtrahend = subtrahend;
        }
    }

    public class MultiplyArguments : IOperationArguments
    {
        public double[] Factors { get; set; } = Array.Empty<double>();

        public MultiplyArguments() { }

        public MultiplyArguments(double[] factors)
        {
            Factors = factors;
        }
    }

    public class DivisionArguments : IOperationArguments
    {
        public double Dividend { get; set; }
        public double Divisor { get; set; }

        public DivisionArguments() { }

        public DivisionArguments(double dividend, double divisor)
        {
            this.Dividend = dividend;
            this.Divisor = divisor;
        }
    }

    public class SquareRootArguments : IOperationArguments
    {
        public double Number { get; set; }
        public SquareRootArguments(double number)
        {
            this.Number = number;
        }
    }
}