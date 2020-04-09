using System;

namespace Budgee.DailyBudgets.Messages.DailyBudgets
{
    public static class Events
    {
        public abstract class Event
        {
            public DateTime EntryDate { get; set; }
        }
        public class DailyBudgetCreated : Event
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
        public class IncomeAddedToDailyBudget : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid IncomeId { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
        }
        public class ExtraIncomeAddedToDailyBudget : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid IncomeId { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
        }
        public class IncomeAmountChanged : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid IncomeId { get; set; }
            public decimal Amount { get; set; }
        }
        public class IncomeRemoved : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid IncomeId { get; set; }
        }

        public class FixedOutgoAddedToDailyBudget : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid OutgoId { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
        }
        public class VariableOutgoAddedToDailyBudget : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid OutgoId { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
        }
        public class OutgoAmountChanged : Event
        {
            public decimal Daily { get; set; }
            public Guid DailyBudgetId { get; set; }
            public Guid OutgoId { get; set; }
            public decimal Amount { get; set; }
        }

        public class OutgoRemoved : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid OutgoId { get; set; }
        }

        public class PeriodAddedToDailyBudget : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid PeriodId { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }
        public class PeriodStartChanged : Event
        {
            public DateTime Start { get; set; }
            public Guid DailyBudgetId { get; set; }
        }
        public class PeriodEndChanged : Event
        {
            public DateTime End { get; set; }
            public Guid DailyBudgetId { get; set; }
        }
        public class MustHaveSpendingAdded : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid SpendingId { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
        }
        public class NiceToHaveSpendingAdded : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid SpendingId { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
        }
        public class SnapshotChanged : Event
        {
            public Guid DailyBudgetId { get; set; }
            public decimal Daily { get; set; }
            public decimal Available { get; set; }
        }
        public class KeyNumberChanged : Event
        {
            public Guid DailyBudgetId { get; set; }
            public (string, float)[] KeyNumbers { get; set; }
        }


    }
}
