using System;

namespace Budgee.Domain.DailyBudgets
{
    public static class PeriodOperations
    {
        public static int ElapsedDays(this Period period, DateTime present)
        {
            return (int)((present - period.FromA).TotalDays) + 1;
        }
    }
}
