using CalculatorService.Server.Models;
using CalculatorService.Server.Services.Interfaces;
using CalculatorService.Server.Utils.Interfaces;
using System.Collections.Concurrent;

namespace CalculatorService.Server.Services
{
    /// <summary>
    /// Class responsible for storing and retrieving operations in memory 
    /// </summary>
    public class JournalService : IJournalService
    {
        private readonly ILogging _logging;
        private readonly ConcurrentQueue<JournalItem> _cache;

        public JournalService(ILogging logging)
        {
            _logging = logging;
            _cache = new ConcurrentQueue<JournalItem>();
        }

        public void Save(string id, string operation, string calculation, DateTime date)
        {
            _logging.Information($"Saving information, traking id: {id}");
            var journalItem = new JournalItem(id, operation, calculation, date);
            _cache.Enqueue(journalItem);
        }

        public JournalItem[] GetJournalItemsById(string id)
        {
            return _cache.Where(x => x.Id == id).ToArray();
        }
    }
}