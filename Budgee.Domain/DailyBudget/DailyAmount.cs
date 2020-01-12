using Budgee.Domain.Shared;
using System;

namespace Budgee.Domain.DailyBudget
{
    public sealed class DailyAmount : Money
    {
        private DailyAmount(decimal amount) : base(amount)
        {}
        public new static DailyAmount FromDecimal(decimal amount) => new DailyAmount(amount);
        public static implicit operator decimal(DailyAmount self) => self.Amount;
        public static bool operator <(DailyAmount a, DailyAmount b) => a.Amount < b.Amount;
        public static bool operator >(DailyAmount a, DailyAmount b) => a.Amount > b.Amount;
    }
}
