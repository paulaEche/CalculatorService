using CalculatorService.Server.Models;

namespace CalculatorService.Server.Services.Interfaces
{
    public interface IJournalService
    {
        JournalItem[] GetJournalItemsById(string id);
        void Save(string id, string operation, string calculation, DateTime date);
    }
}