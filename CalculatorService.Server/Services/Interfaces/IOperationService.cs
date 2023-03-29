using CalculatorService.Server.Models.Interfaces;

namespace CalculatorService.Server.Services.Interfaces
{
    public interface IOperationService
    {
        public IOperationResult? GetOperationResult(IOperationArguments operands);
    }
}