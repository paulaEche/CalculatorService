using CalculatorService.Server.Controllers;
using CalculatorService.Server.Models;
using CalculatorService.Server.Services.Interfaces;
using CalculatorService.Server.Utils.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CalculatorService.Test
{
    public class JournalControllerTest
    {
        private JournalController _journalControllerTest;

        public JournalControllerTest()
        {
            SetUpController();
        }

        private void SetUpController()
        {
            var logging = new Mock<ILogging>();
            var journalService = new Mock<IJournalService>();
            var journalItems = new JournalItem[2]
            {
                new JournalItem("1", "Add", "1 + 1 = 2", DateTime.Now),
                new JournalItem("1", "Subtract", "1 - 1 = 0", DateTime.Now)
            };
            journalService.Setup(x => x.GetJournalItemsById("1")).Returns(journalItems);

            _journalControllerTest = new JournalController(journalService.Object, logging.Object);
        }

        [Fact]
        public void Journal_Controller()
        {
            var result = _journalControllerTest.GetQuery("1");

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ActionResult<JournalItem[]>>(result);

            var value = result.Result as OkObjectResult;
            Assert.IsType<JournalItem[]>(value.Value);
            Assert.Equal(2, (value.Value as JournalItem[]).Length);
            Assert.Equal("1", (value.Value as JournalItem[])[0].Id);
        }
    }
}