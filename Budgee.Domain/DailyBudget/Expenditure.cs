using System;

namespace Budgee.Domain.DailyBudget
{
    public sealed class Expenditure : Outgo
    {
        public Expenditure(Action<object> applier) : base(applier)
        {}
    }
}
