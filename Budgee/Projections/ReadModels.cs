using System;
using System.Collections.Generic;
using System.Text;

namespace Budgee.Projections
{
    public static class ReadModels
    {
        public class DailyBudgets
        {
            public Guid DailyBudgtId { get; set; }
            public decimal Remaining { get; set; }
            public decimal DailyAmount { get; set; }
            public DateTime Start { get; set; }
            public DateTime End{ get; set; }
        }
    }
}
