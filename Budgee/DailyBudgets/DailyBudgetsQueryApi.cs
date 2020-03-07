using Budgee.DailyBudgets.Messages;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;


namespace Budgee.DailyBudgets.DailyBudgets
{
    [ApiController, Route("/budget")]
    public sealed class DailyBudgetQueryApi : ControllerBase
    {
        private static ILogger log = Log.ForContext<DailyBudgetQueryApi>();
        private readonly IEnumerable<ReadModels.DailyBudgets> items;

        public DailyBudgetQueryApi(IEnumerable<ReadModels.DailyBudgets> items)
        {
            this.items = items;
        }
        [HttpGet]
        public IActionResult Get(
           [FromQuery] QueryModels.GetDailyBudget request)
                => RequestHandler.HandleQuery(() => items.Query(request), log);
    }
}
