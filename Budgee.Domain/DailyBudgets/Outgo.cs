using Budgee.DailyBudgets.Domain.DailyBudgets;
using Budgee.DailyBudgets.Messages.DailyBudgets;
using Budgee.Framework;
using System;

namespace Budgee.Domain.DailyBudgets
{
    public class Outgo : Entity<OutgoId>
    {
        public Outgo(Action<object> applier) : base(applier)
        {}
        public DailyBudgetId ParentId { get; protected set; }
        public Amount Amount { get; protected set; }
        public Description Description { get; set; }
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.OutgoAddedToDailyBudget e:
                    ParentId = new DailyBudgetId(e.DailyBudgetId);
                    Id = new OutgoId(e.OutgoId);
                    Amount = Amount.FromDecimal(e.Amount);
                    Description = new Description(e.Description);
                    break;
                case Events.OutgoAmountChanged e:
                    Amount = Amount.FromDecimal(e.Amount);
                    break;
                case Events.OutgoDescriptionChanged e:
                    Description = new Description(e.Description);
                    break;
            }
        }
    }

    public class OutgoId : Value<OutgoId>
    {
        private readonly Guid value;

        public OutgoId(Guid value) => this.value = value;

        protected override bool CompareProperties(OutgoId other)
            => value == other.value;
        public static implicit operator Guid(OutgoId self)=> self.value;
    }
}
