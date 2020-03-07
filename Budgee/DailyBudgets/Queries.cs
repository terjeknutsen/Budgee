<<<<<<< HEAD
﻿using Budgee.DailyBudgets.Messages;
using System.Collections.Generic;
using System.Linq;
=======
﻿using Budgee.Domain.DailyBudgets;
using Budgee.Projections;
using System.Threading.Tasks;
>>>>>>> 4cfac43ef23ba3f92c02fb306d94fb193648e2be

namespace Budgee.DailyBudgets.DailyBudgets
{
    public static class Queries
    {
<<<<<<< HEAD
        public static ReadModels.DailyBudgets Query(
        this IEnumerable<ReadModels.DailyBudgets> items, QueryModels.GetDailyBudget query)
        => items.FirstOrDefault(x => x.DailyBudgtId == query.DailyBudgetId);
        //public static ReadModels.Snapshots Query(
        //this IEnumerable<ReadModels.Snapshots> items, QueryModels.GetSnapshot query)
        //   => items.FirstOrDefault(x => x.SnapshotId == query.SnapshotId);
=======
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
>>>>>>> 4cfac43ef23ba3f92c02fb306d94fb193648e2be
    }
}
