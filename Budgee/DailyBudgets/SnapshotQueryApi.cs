using Budgee.Projections;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Budgee.DailyBudgets.DailyBudgets
{
    [ApiController,Route("/snapshot")]
    public sealed class SnapshotQueryApi : ControllerBase
    {
        private readonly IEnumerable<ReadModels.Snapshots> items;
        public SnapshotQueryApi(IEnumerable<ReadModels.Snapshots> items)
        {
            this.items = items;
        }
        [HttpGet]
        public IActionResult Get(
            QueryModels.GetSnapshot query)
             => RequestHandler.HandleQuery(()=> items.Query(query));
    }
}
