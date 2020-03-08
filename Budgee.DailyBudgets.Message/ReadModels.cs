using System;
using System.Collections.Generic;

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
            public IList<Expenditures> Expenditures { get; set; }
            public IList<Incomes> Incomes { get; set; }
            public IList<Outgos> Outgos { get; set; }
        }
        public class Snapshots
        {
            public Guid DailyBudgetId { get; set; }
            public Guid SnapshotId { get; set; }
            public decimal Daily { get; set; }
            public decimal Available { get; set; }
        }
        public class Expenditures
        {
            public Guid DailyBudgetId { get; set; }
            public Guid ExpenditureId { get; set; }
            public decimal Amount{ get; set; }
            public string Description { get; set; }
        }
        public class Incomes{
            public Guid DailyBudgetId { get; set; }
            public Guid IncomeId { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
        }
        public class Outgos 
        {
            public Guid DailyBudgetId { get; set; }
            public Guid OutgoId { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
        }
    }
}
