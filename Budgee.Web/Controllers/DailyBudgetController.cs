using System.Threading.Tasks;
using Budgee.Framework;
using Microsoft.AspNetCore.Mvc;
using static Budgee.DailyBudgets.Messages.DailyBudgets.Commands;

namespace Budgee.Controllers
{
    [Route("api/dailyBudget")]
    [ApiController]
    public class DailyBudgetController : ControllerBase
    {
        private readonly IApplicationService applicationService;

        public DailyBudgetController(IApplicationService applicationService)
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
        [Route("change/income")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.ChangeIncome request)
        {
            await applicationService.Handle(request);
            return Ok();
        }
        [Route("change/outgo")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.ChangeOutgo request)
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