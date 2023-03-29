using CalculatorService.Server.Models.Interfaces;

namespace CalculatorService.Server.Models
{
    public class AddArguments : IOperationArguments
    {
        public readonly double[] addends;

        public AddArguments(double[] operands)
        {
            this.addends = operands;
        }
    }

    public class SubtractArguments : IOperationArguments
    {
        public readonly double minuend;
        public readonly double subtrahend;
        public SubtractArguments(double operand1, double operand2)
        {
            minuend = operand1;
            subtrahend = operand2;
        }
    }

    public class MultiplyArguments : IOperationArguments
    {
        public readonly double[] factors;

        public MultiplyArguments(double[] operands)
        {
            this.factors = operands;
        }
    }

    public class DivisionArguments : IOperationArguments
    {
        public readonly double dividend;
        public readonly double divisor;
        public DivisionArguments(double operand1, double operand2)
        {
            dividend = operand1;
            divisor = operand2;
        }
    }

    public class SquareRootArguments : IOperationArguments
    {
        public readonly double number;
        public SquareRootArguments(double operand)
        {
            number = operand;
        }
    }
}