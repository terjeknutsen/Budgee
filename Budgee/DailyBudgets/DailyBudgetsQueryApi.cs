using Budgee.Domain.DailyBudgets;
using Budgee.Projections;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Budgee.DailyBudgets.DailyBudgets
{
    [ApiController, Route("/snapshot")]
    public sealed class DailyBudgetsQueryApi : ControllerBase
    {
        private readonly IDailyBudgetRepository repository;

        public DailyBudgetsQueryApi(IDailyBudgetRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<ReadModels.Snapshots>> Get(
        [FromQuery] QueryModels.GetSnapshot query)
        {
            return new OkObjectResult(await Queries.Query(repository, query)); 
        }
    }
}
