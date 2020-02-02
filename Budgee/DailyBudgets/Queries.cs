using Budgee.Domain.DailyBudgets;
using Budgee.Projections;
using System.Threading.Tasks;

namespace Budgee.DailyBudgets.DailyBudgets
{
    public static class Queries
    {
        public static async Task<ReadModels.Snapshots> Query(
        IDailyBudgetRepository repository, QueryModels.GetSnapshot query)
        {
            var entity = await repository.Load(new DailyBudgetId(query.DailyBudgetId));
            return new ReadModels.Snapshots 
            { 
                DailyBudgetId = entity.Id, 
                SnapshotId = entity.Snapshot.Id, 
                Available = entity.Snapshot.Available, 
                Daily = entity.Snapshot.Daily 
            };
        }
    }
}
