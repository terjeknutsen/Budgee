﻿using System;

namespace Budgee.Projections
{
    public static class ReadModels
    {
        public class DailyBudgets
        {
            public Guid DailyBudgtId { get; set; }
<<<<<<< HEAD
            public string Name { get; set; }
=======
>>>>>>> 4cfac43ef23ba3f92c02fb306d94fb193648e2be
            public decimal Remaining { get; set; }
            public decimal DailyAmount { get; set; }
            public DateTime Start { get; set; }
            public DateTime End{ get; set; }
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
