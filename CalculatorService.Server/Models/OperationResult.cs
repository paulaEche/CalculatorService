using CalculatorService.Server.Models.Interfaces;

namespace CalculatorService.Server.Models
{
    public class AddResult : IOperationResult
    {
        public AddResult(double sum)
        {
            Sum = sum;
        }

        public double Sum { get; }
    }

    public class SubtractResult : IOperationResult
    {
        public SubtractResult(double difference)
        {
            Difference = difference;
        }

        public double Difference { get; }
    }

    public class MultiplyResult : IOperationResult
    {
        public MultiplyResult(double product)
        {
            Product = product;
        }

        public double Product { get; }
    }

    public class DivisionResult : IOperationResult
    {
        public DivisionResult(double quotient, double remainder)
        {
            Quotient = quotient;
            Remainder = remainder;
        }

        public double Quotient { get; }
        public double Remainder { get; }
    }


    public class SquareRootResult : IOperationResult
    {
        public SquareRootResult(double squareRoot)
        {
            SquareRoot = squareRoot;
        }

        public double SquareRoot { get; }
    }
}