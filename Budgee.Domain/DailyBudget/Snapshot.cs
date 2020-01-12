using Budgee.Framework;
using System;

namespace Budgee.Domain.DailyBudget
{
    public sealed class Snapshot : Entity<SnapshotId>
    {
        private Period period;
        private Amount totalIncome;
        private Amount totalOutgo;
        private Amount totalExpenditure;

        public Snapshot(Action<object> applier) : base(applier)
        { }
        public DailyBudgetId ParentId{ get; private set; }

        public Available Available { get; private set; }

       

        public DailyAmount Daily { get; private set; }

        internal void Update(Amount income)
            => Apply(new Events.TotalIncomeChanged
            {
                DailyBudgetId = ParentId,
                TotalIncome = income
            });
        internal void UpdateOutgo(Amount outgo)
            => Apply(new Events.TotalOutgoChanged
               {
                DailyBudgetId = ParentId,
                TotalOutgo = outgo
               });

        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.DailyBudgetCreated e:
                    ParentId = new DailyBudgetId(e.Id);
                    Id = new SnapshotId(Guid.NewGuid());
                    Daily = DailyAmount.FromDecimal(0m);
                    Available = Available.FromDecimal(0m);

                    break;
                case Events.PeriodAddedToDailyBudget e:
                    period = Period.Create(e.Start, e.End);
                    break;
                case Events.TotalIncomeChanged e:
                    totalIncome = Amount.FromDecimal(e.TotalIncome);
                    Daily = CalculateDaily(period,totalIncome,totalOutgo);
                    Available = CalculateAvailable();
                    break;
                case Events.TotalOutgoChanged e:
                    totalOutgo = Amount.FromDecimal(e.TotalOutgo);
                    Daily = CalculateDaily(period, totalIncome, totalOutgo);
                    break;
                case Events.PeriodStartChanged e:
                    period = Period.Create(e.Start, period.ToB);
                    Daily = CalculateDaily(period, totalIncome, totalOutgo);
                    break;
                case Events.PeriodEndChanged e:
                    period = Period.Create(period.FromA, e.End);
                    Daily = CalculateDaily(period, totalIncome, totalOutgo);
                    break;
            }
        }

        private static DailyAmount CalculateDaily(Period period, Amount totalIncome, Amount totalOutgo)
        {
            if (period == null) return DailyAmount.FromDecimal(0m);
            var income = totalIncome?.Amount ?? 0m;
            var outgo = totalOutgo?.Amount ?? 0m;
            var daily = Math.Round(((income - outgo) / period.Days),2);
            
            return DailyAmount.FromDecimal(daily);
        }
        private Available CalculateAvailable()
        {
            totalExpenditure = Amount.FromDecimal(0m);
            var days = period.Days;
            var dailyAmount = Daily.Amount;
            var totalExpenditure = totalExpenditure;
            
        }


    }

    public sealed class SnapshotId : Value<SnapshotId>
    {
        private readonly Guid value;

        public SnapshotId(Guid value)
         =>   this.value = value;
        protected override bool CompareProperties(SnapshotId other)
         =>   value == other.value;
    }
}
