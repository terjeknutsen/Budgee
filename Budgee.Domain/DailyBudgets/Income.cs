using Budgee.DailyBudgets.Messages.DailyBudgets;
using Budgee.Framework;
using System;

namespace Budgee.Domain.DailyBudgets
{
    public sealed class Income : Entity<IncomeId>
    {
        public Income(Action<object> applier) : base(applier)
        {}
        public DailyBudgetId ParentId { get; private set; }
        public Amount Amount { get; private set; }
        public void ChangeAmount(Amount newAmount)
            => Apply(new Events.IncomeAmountChanged
            {
                DailyBudgetId = ParentId,
                IncomeId = Id,
                Amount = newAmount.Amount
            });
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.IncomeAddedToDailyBudget e:
                    ParentId = new DailyBudgetId(e.DailyBudgetId);
                    Id = new IncomeId(e.IncomeId);
                    Amount = Amount.FromDecimal(e.Amount);
                    break;
                case Events.IncomeAmountChanged e:
                    Amount = Amount.FromDecimal(e.Amount);
                    break;
            }
        
        }
    }

    public sealed class IncomeId : Value<IncomeId>
    {
        private readonly Guid value;

        public IncomeId(Guid value) => this.value = value;

        protected override bool CompareProperties(IncomeId other)
            => value == other.value;
        public static implicit operator Guid(IncomeId self) => self.value;
    }
}
