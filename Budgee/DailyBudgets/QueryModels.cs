using System;
using System.Collections.Generic;
using System.Text;

namespace Budgee.DailyBudgets.Messages
{
    public static class QueryModels
    {
        public class GetSnapshot
        {
            public Guid SnapshotId { get; set; }
        }
        public class GetDailyBudget
        {
            public Guid DailyBudgetId { get; set; }
            public override string ToString()
            {
                return nameof(DailyBudgetId) + "=" + DailyBudgetId;
            }
        }

        public class GetDailyBudgets
        { }
    }
}
