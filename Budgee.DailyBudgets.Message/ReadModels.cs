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
            public IList<Expenditures> Expenditures { get; set; } = new List<Expenditures>();
            public IList<Incomes> Incomes { get; set; } = new List<Incomes>();
            public IList<Outgos> Outgos { get; set; } = new List<Outgos>();
            public IList<KeyNumber> KeyNumbers { get; set; } = new List<KeyNumber>();
            public decimal TotalExpenditure { get; set; }
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
            public decimal Amount { get; set; }
            public string Description { get; set; }
        }
        public class Incomes
        {
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
        public class KeyNumber
        {
            public string Type { get; set; }
            public float Value { get; set; }
        }
    }
}
