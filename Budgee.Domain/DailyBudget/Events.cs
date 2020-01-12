using System;

namespace Budgee.Domain.DailyBudget
{
    public static class Events
    {
        public class DailyBudgetCreated
        {
            public Guid Id { get; set; }
        } 
        public class IncomeAddedToDailyBudget
        {
            public Guid DailyBudgetId { get; set; }
            public Guid IncomeId { get; set; }
            public decimal Amount { get; set; }
            public decimal DailyAmount{ get; set; }

        }
        public class IncomeAmountChanged 
        {
            public Guid DailyBudgetId { get; set; }
            public Guid IncomeId { get; set; }
            public decimal Amount { get; set; }
        }
        public class IncomeRemoved
        {
            public Guid DailyBudgetId{ get; set; }
            public Guid IncomeId { get; set; }
        }
        public class TotalIncomeChanged
        {
            public Guid DailyBudgetId{ get; set; }
            public decimal TotalIncome { get; set; }
        }


        public class OutgoAddedToDailyBudget
        {
            public Guid DailyBudgetId { get; set; }
            public Guid OutgoId { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; internal set; }
        }
        public class OutgoAmountChanged
        {
            public Guid DailyBudgetId{ get; set; }
            public Guid OutgoId { get; set; }
            public decimal Amount { get; set; }
        }

        public class OutgoRemoved
        {
            public Guid DailyBudgetId { get; set; }
            public Guid OutgoId { get; set; }
        }
        public class TotalOutgoChanged
        {
            public Guid DailyBudgetId { get; set; }
            public decimal TotalOutgo { get; set; }
        } 

        public class DailyAmountChanged
        {
            public Guid DailyBudgetId { get; set; }
        } 
        public class PeriodAddedToDailyBudget
        {
            public Guid DailyBudgetId { get; set; }
            public Guid PeriodId { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }
        public class PeriodStartChanged
        {
            public DateTime Start { get; set; }
        }
        public class PeriodEndChanged
        {
            public DateTime End { get; set; } 
        }
    } 
}
