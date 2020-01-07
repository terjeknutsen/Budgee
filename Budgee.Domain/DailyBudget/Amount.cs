using Budgee.Domain.Shared;
using System;

namespace Budgee.Domain.DailyBudget
{
    public sealed class Amount : Money
    {
        private Amount(decimal amount) : base(amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative", nameof(amount));
        }

        public new static Amount FromDecimal(decimal amount) => new Amount(amount);
        public static implicit operator decimal(Amount self) => self.Amount;
    }
}
