using Budgee.Domain.Shared;
using System;

namespace Budgee.Domain.DailyBudgets
{
    public sealed class Available : Money
    {
        private Available(decimal amount) : base(amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Available cannot be negative");
        }
        public new static Available FromDecimal(decimal amount) => new Available(amount);
        public static bool operator <(Available a, Available b) => a.Amount < b.Amount;
        public static bool operator >(Available a, Available b) => a.Amount > b.Amount;
    }
}
