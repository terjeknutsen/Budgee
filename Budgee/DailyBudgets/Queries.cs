using Budgee.DailyBudgets.Messages;
using System.Collections.Generic;
using System.Linq;

namespace Budgee.DailyBudgets.DailyBudgets
{
    public static class Queries
    {
        public static ReadModels.DailyBudgets Query(
        this IEnumerable<ReadModels.DailyBudgets> items, QueryModels.GetDailyBudget query)
        => items.FirstOrDefault(x => x.DailyBudgtId == query.DailyBudgetId);
        //public static ReadModels.Snapshots Query(
        //this IEnumerable<ReadModels.Snapshots> items, QueryModels.GetSnapshot query)
        //   => items.FirstOrDefault(x => x.SnapshotId == query.SnapshotId);
    }
}
