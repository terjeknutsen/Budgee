using System;

namespace Budgee.DailyBudgets.DailyBudgets
{
    public static class QueryModels
    {
        public class GetSnapshot
        {
            public Guid DailyBudgetId{ get; set; }
        }
    }
}
