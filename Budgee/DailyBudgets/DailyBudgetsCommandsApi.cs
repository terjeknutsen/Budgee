using Budgee.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Budgee.DailyBudgets.Messages.DailyBudgets.Commands;

namespace Budgee.DailyBudgets
{
    [Route("api/dailyBudget")]
    [ApiController]
    public class DailyBudgetsCommandsApi : ControllerBase
    {
        private readonly IApplicationService applicationService;

        public DailyBudgetsCommandsApi(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(V1.Create request)
        {
            await applicationService.Handle(request);
            return Ok();
        }
        [Route("income")]
        [HttpPost]
        public async Task<IActionResult> Post(V1.AddIncome request)
        {
            await applicationService.Handle(request);
            return Ok();
        }
        [Route("outgo")]
        [HttpPost]
        public async Task<IActionResult> Post(V1.AddOutgo request)
        {
            await applicationService.Handle(request);
            return Ok();
        }
        [Route("period")]
        [HttpPost]
        public async Task<IActionResult> Post(V1.SetPeriod request)
        {
            await applicationService.Handle(request);
            return Ok();
        }
        [Route("expenditure")]
        [HttpPost]
        public async Task<IActionResult> Post(V1.AddExpenditure request)
        {
            await applicationService.Handle(request);
            return Ok();
        }
        [Route("change/income/amount")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.ChangeIncomeAmount request)
        {
            await applicationService.Handle(request);
            return Ok();
        }
        [Route("change/income/description")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.ChangeIncomeDescription request)
        {
            await applicationService.Handle(request);
            return Ok();
        }

        [Route("change/outgo/amount")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.ChangeOutgoAmount request)
        {
            await applicationService.Handle(request);
            return Ok();
        }
        [Route("change/outgo/description")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.ChangeOutgoDescription request)
        {
            await applicationService.Handle(request);
            return Ok();
        }
        [Route("change/expenditure/amount")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.ChangeExpenditureAmount request)
        {
            await applicationService.Handle(request);
            return Ok();
        }
        [Route("change/expenditure/description")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.ChangeExpenditureDescription request)
        {
            await applicationService.Handle(request);
            return Ok();
        }
        [Route("change/start")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.ChangeStart request)
        {
            await applicationService.Handle(request);
            return Ok();
        }
        [Route("change/end")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.ChangeEnd request)
        {
            await applicationService.Handle(request);
            return Ok();
        }
        [Route("remove/income")]
        [HttpDelete]
        public async Task<IActionResult> Delete(V1.RemoveIncome request)
        {
            await applicationService.Handle(request);
            return Ok();
        }
        [Route("remove/outgo")]
        [HttpDelete]
        public async Task<IActionResult> Delete(V1.RemoveOuto request)
        {
            await applicationService.Handle(request);
            return Ok();
        }
    }
}