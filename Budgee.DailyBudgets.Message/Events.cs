using System;
using System.Collections.Generic;
using System.Text;

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
        }
        public class IncomeAddedToDailyBudget : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid IncomeId { get; set; }
            public decimal Amount { get; set; }
            public decimal DailyAmount { get; set; }

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
        public class TotalIncomeChanged : Event
        {
            public Guid DailyBudgetId { get; set; }
            public decimal TotalIncome { get; set; }
        }


        public class OutgoAddedToDailyBudget : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid OutgoId { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; internal set; }
        }
        public class OutgoAmountChanged : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid OutgoId { get; set; }
            public decimal Amount { get; set; }
        }

        public class OutgoRemoved : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid OutgoId { get; set; }
        }
        public class TotalOutgoChanged : Event
        {
            public Guid DailyBudgetId { get; set; }
            public decimal TotalOutgo { get; set; }
        }
        public class TotalExpenditureChanged : Event
        {
            public Guid DailyBudgetId { get; set; }
            public decimal TotalExpenditure { get; set; }
        }

        public class DailyAmountChanged : Event
        {
            public Guid DailyBudgetId { get; set; }
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
        }
        public class PeriodEndChanged : Event
        {
            public DateTime End { get; set; }
        }
        public class ExpenditureAdded : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid ExpenditureId { get; set; }
            public decimal Amount { get; set; }
        }
        public class SnapshotChanged : Event
        {
            public Guid DailyBudgetId { get; set; }
            public Guid SnapshotId { get; set; }
            public decimal Daily { get; set; }
            public decimal Available { get; set; }
        }
    }
}
