using System;

namespace Budgee.DailyBudgets.Messages
{
    public static class ReadModels
    {
        public class DailyBudgets
        {
            public Guid DailyBudgtId { get; set; }
            public string Name { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public Snapshots Snapshot { get; set; }
        }
        public class Snapshots
        {
            public Guid DailyBudgetId { get; set; }
            public Guid SnapshotId { get; set; }
            public decimal Daily { get; set; }
            public decimal Available { get; set; }
        }
    }
}
