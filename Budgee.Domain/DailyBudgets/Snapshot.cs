using Budgee.Framework;
using System;

namespace Budgee.Domain.DailyBudgets
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

        internal void Update(Amount income,DateTime entryDate)
            => Apply(new Events.TotalIncomeChanged
            {
                DailyBudgetId = ParentId,
                TotalIncome = income,
                EntryDate = entryDate
            });
        internal void UpdateOutgo(Amount outgo,DateTime entryDate)
            => Apply(new Events.TotalOutgoChanged
               {
                DailyBudgetId = ParentId,
                TotalOutgo = outgo,
                EntryDate = entryDate
               });
        internal void UpdateTotalExpenditure(Amount expenditures, DateTime entryDate)
            => Apply(new Events.TotalExpenditureChanged
            {
                DailyBudgetId = ParentId,
                TotalExpenditure = expenditures,
                EntryDate = entryDate
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
                    Available = CalculateAvailable(period,e.EntryDate,Daily,totalExpenditure);
                    break;
                case Events.TotalOutgoChanged e:
                    totalOutgo = Amount.FromDecimal(e.TotalOutgo);
                    Daily = CalculateDaily(period, totalIncome, totalOutgo);
                    Available = CalculateAvailable(period, e.EntryDate, Daily, totalExpenditure);
                    break;
                case Events.PeriodStartChanged e:
                    period = Period.Create(e.Start, period.ToB);
                    Daily = CalculateDaily(period, totalIncome, totalOutgo);
                    break;
                case Events.PeriodEndChanged e:
                    period = Period.Create(period.FromA, e.End);
                    Daily = CalculateDaily(period, totalIncome, totalOutgo);
                    break;
                case Events.TotalExpenditureChanged e:
                    totalExpenditure = Amount.FromDecimal(e.TotalExpenditure);
                    Available = CalculateAvailable(period, e.EntryDate, Daily, totalExpenditure);
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
        private static Available CalculateAvailable(Period period,DateTime timeOfChange,DailyAmount dailyAmount, Amount totalExpenditure)
        {
            if (period == null) return Available.FromDecimal(0m);
            var expenditures = totalExpenditure?.Amount ?? 0m;
            var elapsedDays = period.ElapsedDays(timeOfChange);
            var available = (elapsedDays * dailyAmount) - expenditures;
            return Available.FromDecimal(available);
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
