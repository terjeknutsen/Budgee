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
                public double Income { get; set; }
                public double Outgo { get; set; }

            }
            public class AddIncome
            {
                public Guid DailyBudgetId { get; set; }
                public Guid IncomeId { get; set; }
                public double Amount { get; set; }
                public string Description { get; set; }
            }

            public class ChangeIncome
            {
                public Guid DailyBudgetId { get; set; }
                public Guid IncomeId { get; set; }
                public double Amount { get; set; }
                public string Description { get; set; }
                public string Type { get; set; }
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
            public class AddOutgo
            {
                public Guid DailyBudgetId { get; set; }
                public Guid OutgoId { get; set; }
                public double Amount { get; set; }
                public string Description { get; set; }
            }
            public class ChangeOutgo
            {
                public Guid DailyBudgetId { get; set; }
                public Guid OutgoId { get; set; }
                public double Amount { get; set; }
                public string Description { get; set; }
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

            public class AddSpending
            {
                public Guid DailyBudgetId { get; set; }
                public Guid SpendingId { get; set; }
                public string Description { get; set; }
                public double Amount { get; set; }
            }
            public class ChangeSpending
            {
                public Guid DailyBudgetId { get; set; }
                public Guid SpendingId { get; set; }
                public double Amount { get; set; }
                public string Description { get; set; }
            }
            public class RemoveSpending
            {
                public Guid DailyBudgetId { get; set; }
                public Guid SpendingId { get; set; }
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
