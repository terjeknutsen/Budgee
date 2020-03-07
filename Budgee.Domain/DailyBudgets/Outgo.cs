<<<<<<< HEAD:Budgee.Domain/DailyBudgets/Outgo.cs
﻿using Budgee.DailyBudgets.Domain.DailyBudgets;
using Budgee.DailyBudgets.Messages.DailyBudgets;
=======
﻿using Budgee.DailyBudgets.Messages.DailyBudgets;
>>>>>>> 4cfac43ef23ba3f92c02fb306d94fb193648e2be:Budgee.Domain/DailyBudgets/Outgo.cs
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
<<<<<<< HEAD:Budgee.Domain/DailyBudgets/Outgo.cs
        public Description Description { get; set; }
=======
>>>>>>> 4cfac43ef23ba3f92c02fb306d94fb193648e2be:Budgee.Domain/DailyBudgets/Outgo.cs
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
