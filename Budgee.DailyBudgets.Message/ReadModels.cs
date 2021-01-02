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
            public IList<Spendings> Spendings { get; set; } = new List<Spendings>();
            public IList<Incomes> Incomes { get; set; } = new List<Incomes>();
            public IList<Outgos> Outgos { get; set; } = new List<Outgos>();
            public IList<KeyNumber> KeyNumbers { get; set; } = new List<KeyNumber>();
            public double TotalSpendings { get; set; }
        }
        public class Snapshots
        {
            public Guid DailyBudgetId { get; set; }
            public Guid SnapshotId { get; set; }
            public double Daily { get; set; }
            public double Available { get; set; }
            public double LiveAvailable { get; set; }
        }
        public class Spendings
        {
            public Guid DailyBudgetId { get; set; }
            public Guid SpendingId { get; set; }
            public double Amount { get; set; }
            public string Description { get; set; }
            public DateTime Entry { get; set; }
        }
        public class Incomes
        {
            public Guid DailyBudgetId { get; set; }
            public Guid IncomeId { get; set; }
            public double Amount { get; set; }
            public string Description { get; set; }
            public DateTime Entry { get; set; }
        }
        public class Outgos
        {
            public Guid DailyBudgetId { get; set; }
            public Guid OutgoId { get; set; }
            public double Amount { get; set; }
            public string Description { get; set; }
            public DateTime Entry { get; set; }
        }
        public class KeyNumber
        {
            public string Type { get; set; }
            public float Value { get; set; }
        }
    }
}
