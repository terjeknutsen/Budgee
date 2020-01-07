using Budgee.Domain.Shared;
using System;

namespace Budgee.Domain.DailyBudget
{
    public sealed class DailyAmount : Money
    {
        private DailyAmount(decimal amount) : base(amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("Amount cannot be negative");
        }
        public new static DailyAmount FromDecimal(decimal amount) => new DailyAmount(amount);
    }
}
