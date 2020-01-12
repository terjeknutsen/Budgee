using Budgee.Domain.Shared;
using System;

namespace Budgee.Domain.DailyBudget
{
    public sealed class Available : Money
    {
        private Available(decimal amount) : base(amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Available cannot be negative");
        }
        public new static Available FromDecimal(decimal amount) => new Available(amount);
    }
}
