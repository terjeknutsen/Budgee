using Budgee.DailyBudgets.Messages.DailyBudgets;
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

        private void SetTotalIncome(Amount income, DateTime entryDate)
        {
            totalIncome = income;
            var daily = CalculateDaily();
            var available = CalculateAvailable(entryDate, daily);
            Apply(new Events.SnapshotChanged
            {
                DailyBudgetId = ParentId,
                SnapshotId = Id,
                Daily = daily,
                Available = available,
                EntryDate = entryDate
            });
        }
 
        private void SetTotalOutgo(Amount outgo, DateTime entryDate)
        {
            totalOutgo = outgo;
            var daily = CalculateDaily();
            ApplySnapshotChanges(entryDate, daily);
        }

  

        private void SetTotalExpenditures(Amount expenditures, DateTime entryDate)
        {
            totalExpenditure = expenditures;
            ApplySnapshotChanges(entryDate, Daily);
        }
        private void SetPeriod(DateTime entryDate)
        {
            var daily = CalculateDaily();
            ApplySnapshotChanges(entryDate, daily);
        }
        private void ApplySnapshotChanges(DateTime entryDate, DailyAmount daily)
        {
            var available = CalculateAvailable(entryDate,daily);
            Apply(new Events.SnapshotChanged
            {
                DailyBudgetId = ParentId,
                SnapshotId = Id,
                Daily = daily,
                Available = available,
                EntryDate = entryDate
            });
        }

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
                    SetPeriod(e.EntryDate);
                    break;
                case Events.PeriodStartChanged e:
                    period = Period.Create(e.Start, period.ToB);
                    SetPeriod(e.EntryDate);
                    break;
                case Events.PeriodEndChanged e:
                    period = Period.Create(period.FromA, e.End);
                    SetPeriod(e.EntryDate);
                    break;
                case Events.IncomeAddedToDailyBudget e:
                    SetTotalIncome(Amount.FromDecimal(e.TotalIncome), e.EntryDate);
                    break;
                case Events.IncomeRemoved e:
                    SetTotalIncome(Amount.FromDecimal(e.TotalIncome), e.EntryDate);
                    break;
                case Events.IncomeAmountChanged e:
                    SetTotalIncome(Amount.FromDecimal(e.TotalIncome), e.EntryDate);
                    break;
                case Events.OutgoAddedToDailyBudget e:
                    SetTotalOutgo(Amount.FromDecimal(e.TotalOutgo), e.EntryDate);
                    break;
                case Events.OutgoRemoved e:
                    SetTotalOutgo(Amount.FromDecimal(e.TotalOutgo), e.EntryDate);
                    break;
                case Events.OutgoAmountChanged e:
                    SetTotalOutgo(Amount.FromDecimal(e.TotalOutgo), e.EntryDate);
                    break;
                case Events.ExpenditureAdded e:
                    SetTotalExpenditures(Amount.FromDecimal(e.TotalExpenditure), e.EntryDate);
                    break;
                case Events.SnapshotChanged e:
                    Daily = DailyAmount.FromDecimal(e.Daily);
                    Available = Available.FromDecimal(e.Available);
                    break;
            }
        }


        private DailyAmount CalculateDaily()
        {
            if (period == null) return DailyAmount.FromDecimal(0m);
            var income = totalIncome?.Amount ?? 0m;
            var outgo = totalOutgo?.Amount ?? 0m;
            var daily = Math.Round(((income - outgo) / period.Days),2);
            
            return DailyAmount.FromDecimal(daily);
        }
        private Available CalculateAvailable(DateTime entryDate,DailyAmount dailyAmount)
        {
            if (period == null) return Available.FromDecimal(0m);
            var expenditures = totalExpenditure?.Amount ?? 0m;
            var elapsedDays = period.ElapsedDays(entryDate);
            var available = (elapsedDays * dailyAmount) - expenditures;

            return Available.FromDecimal(available >= 0m ? available : 0m);
        }


    }

    public sealed class SnapshotId : Value<SnapshotId>
    {
        private readonly Guid value;

        public SnapshotId(Guid value)
         =>   this.value = value;
        protected override bool CompareProperties(SnapshotId other)
         =>   value == other.value;
         public static implicit operator Guid(SnapshotId self)=> self.value;
    }
}
