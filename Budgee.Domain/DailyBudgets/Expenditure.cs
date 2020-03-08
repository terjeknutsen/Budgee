using Budgee.DailyBudgets.Messages.DailyBudgets;
using System;

namespace Budgee.Domain.DailyBudgets
{
    public sealed class Expenditure : Outgo
    {
        public Expenditure(Action<object> applier) : base(applier)
        {}
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.ExpenditureAdded e:
                    ParentId = new DailyBudgetId(e.DailyBudgetId);
                    Id = new OutgoId(e.ExpenditureId);
                    Amount = Amount.FromDecimal(e.Amount);
                    break;
                case Events.ExpenditureAmountChanged e:
                    Amount = Amount.FromDecimal(e.Amount);
                    break;
                case Events.ExpenditureDescriptionChanged e:
                    Description = e.Description;
                    break;
            }
        }
    }
}
