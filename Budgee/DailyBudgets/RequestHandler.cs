using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace Budgee.DailyBudgets.DailyBudgets
{
    public class RequestHandler
    {
        public static IActionResult HandleQuery<TModel>(Func<TModel> query, ILogger log)
        {
            try
            {
                return new OkObjectResult(query());

            }
            catch(Exception e)
            {
                log.Error(e, "Error handling the query");
                return new BadRequestObjectResult(new
                {
                    error = e.Message,
                    stackTrace = e.StackTrace
                });
            }
        }
    }
}
