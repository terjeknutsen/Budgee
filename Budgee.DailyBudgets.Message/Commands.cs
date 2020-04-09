using System;

namespace Budgee.DailyBudgets.Messages.DailyBudgets
{
    public static class Commands
    {
        public static class V1
        {
            public class Create
            {
                public Guid DailyBudgetId { get; set; }
                public string BudgetName { get; set; }
                public DateTime Start { get; set; }
                public DateTime End { get; set; }
                public decimal Income { get; set; }
                public decimal Outgo { get; set; }

            }
            public class AddIncome
            {
                public Guid DailyBudgetId { get; set; }
                public Guid IncomeId { get; set; }
                public decimal Amount { get; set; }
                public string Description { get; set; }
            }
            public class AddExtraIncome
            {
                public Guid DailyBudgetId { get; set; }
                public Guid IncomeId { get; set; }
                public decimal Amount { get; set; }
                public string Description { get; set; }
            }
            public class ChangeIncomeAmount
            {
                public Guid DailyBudgetId { get; set; }
                public Guid IncomeId { get; set; }
                public decimal Amount { get; set; }
            }
            public class ChangeIncomeDescription
            {
                public Guid DailyBudgetId { get; set; }
                public Guid IncomeId { get; set; }
                public string Description { get; set; }
            }
            public class RemoveIncome
            {
                public Guid DailyBudgetId { get; set; }
                public Guid IncomeId { get; set; }
            }
            public class AddFixedOutgo
            {
                public Guid DailyBudgetId { get; set; }
                public Guid OutgoId { get; set; }
                public decimal Amount { get; set; }
                public string Description { get; set; }
            }
            public class AddVariableOutgo
            {
                public Guid DailyBudgetId { get; set; }
                public Guid OutgoId { get; set; }
                public decimal Amount { get; set; }
                public string Description { get; set; }
            }
            public class ChangeOutgoAmount
            {
                public Guid DailyBudgetId { get; set; }
                public Guid OutgoId { get; set; }
                public decimal Amount { get; set; }
            }
            public class ChangeOutgoDescription
            {
                public Guid DailyBudgetId { get; set; }
                public Guid OutgoId { get; set; }
                public string Description { get; set; }
            }
            public class RemoveOuto
            {
                public Guid DailyBudgetId { get; set; }
                public Guid OutgoId { get; set; }
            }
            public class AddExpenditure
            {
                public Guid DailyBudgetId { get; set; }
                public Guid ExpenditureId { get; set; }
                public string Description { get; set; }
                public decimal Amount { get; set; }
                public int Kind { get; set; }
            }
            public class ChangeExpenditureAmount
            {
                public Guid DailyBudgetId { get; set; }
                public Guid ExpenditureId { get; set; }
                public decimal Amount { get; set; }
            }
            public class ChangeExpenditureDescription
            {
                public Guid DailyBudgetId { get; set; }
                public Guid ExpenditureId { get; set; }
                public string Description { get; set; }
            }
            public class RemoveExpenditure
            {
                public Guid DailyBudgetId { get; set; }
                public Guid ExpenditureId { get; set; }
            }
            public class SetPeriod
            {
                public Guid DailyBudgetId { get; set; }
                public DateTime Start { get; set; }
                public DateTime End { get; set; }
            }
            public class ChangeStart
            {
                public Guid DailyBudgetId { get; set; }
                public DateTime Start { get; set; }
            }
            public class ChangeEnd
            {
                public Guid DailyBudgetId { get; set; }
                public DateTime End { get; set; }
            }


        }
    }
}
