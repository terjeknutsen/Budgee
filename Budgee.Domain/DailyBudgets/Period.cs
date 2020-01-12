using Budgee.Framework;
using System;

namespace Budgee.Domain.DailyBudgets
{
    public sealed class Period : Value<Period>
    {
        private Period(DateTime fromA, DateTime toB)
        {
            if (fromA > toB) throw new ArgumentOutOfRangeException("Period cannot have negative duration");
            FromA = fromA;
            ToB = toB;
        }
        public DateTime FromA { get; }
        public DateTime ToB { get; }
        public int Days => (int)(ToB - FromA).TotalDays;
        protected override bool CompareProperties(Period other)
            => (FromA, ToB) == (other.FromA, other.ToB);

        public static Period Create(DateTime a, DateTime b) => new Period(a, b);
    }
}
