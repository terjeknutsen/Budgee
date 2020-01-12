using Budgee.Framework;
using System;

namespace Budgee.Domain.DailyBudget
{
    public sealed class Period : Value<Period>
    {
        private readonly DateTime a;
        private readonly DateTime b;

        private Period(DateTime fromA, DateTime toB)
        {
            if (fromA > toB) throw new ArgumentOutOfRangeException("Period cannot have negative duration");
            a = fromA;
            b = toB;
        }
        public DateTime FromA => a;
        public DateTime ToB => b;
        public int Days => (int)(b - a).TotalDays;
        protected override bool CompareProperties(Period other)
            => (a, b) == (other.a, other.b);

        public static Period Create(DateTime a, DateTime b) => new Period(a, b);
    }
}
