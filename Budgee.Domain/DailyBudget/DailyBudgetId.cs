using Budgee.Framework;
using System;

namespace Budgee.Domain.DailyBudget
{
    public sealed class DailyBudgetId : Value<DailyBudgetId>
    {
        private readonly Guid value;

        public DailyBudgetId(Guid value) => this.value = value;
        protected override bool CompareProperties(DailyBudgetId other)
            => value == other.value;
        public static implicit operator Guid(DailyBudgetId self)=> self.value;
        public static implicit operator DailyBudgetId(string value)
            => new DailyBudgetId(Guid.Parse(value));
    }
}
