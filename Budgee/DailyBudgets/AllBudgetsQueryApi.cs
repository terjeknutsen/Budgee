using Budgee.DailyBudgets.Messages;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;

namespace Budgee.DailyBudgets.DailyBudgets
{
    [ApiController, Route("/budgets")]
    public sealed class AllBudgetsQueryApi : ControllerBase
    {
        private static ILogger log = Log.ForContext<AllBudgetsQueryApi>();
        private readonly IEnumerable<ReadModels.DailyBudgets> items;
        public AllBudgetsQueryApi(IEnumerable<ReadModels.DailyBudgets> items)
        {
            this.items = items;
        }
        [HttpGet]
        public IActionResult Get()
            => RequestHandler.HandleQuery(() => items, log);
    }
}
