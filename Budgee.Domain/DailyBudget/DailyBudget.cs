using Budgee.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Budgee.Domain.DailyBudget
{
    public class DailyBudget : AggregateRoot<DailyBudgetId>
    {
        //Needed to keep sqlite database happy
        public DailyBudget()
        {}
        public DailyBudget(DailyBudgetId id)
        {
            Incomes = new List<Income>();
            Outgos = new List<Outgo>();
            Apply(new Events.DailyBudgetCreated { Id = id });
        }
        public void AddIncome(decimal amount)
            => Apply(new Events.IncomeAddedToDailyBudget 
            {
                DailyBudgetId = Id,
                IncomeId = Guid.NewGuid(),
                Amount = amount,
                //Remaining = CalculateRemaining(amount),
                DailyAmount = CalculateAvailable(amount)
            });

     

        public void ChangeIncome(Guid incomeId, decimal amount)
            => Apply(new Events.IncomeAmountChanged 
            {
                DailyBudgetId = Id,
                IncomeId = incomeId,
                Amount = amount
            });
        public void RemoveIncome(Guid incomeId)
            => Apply(new Events.IncomeRemoved
            {
                DailyBudgetId = Id,
                IncomeId = incomeId
            });
        public void AddOutgo(decimal amount)
            => Apply(new Events.OutgoAddedToDailyBudget
            {
                DailyBudgetId = Id,
                OutgoId = Guid.NewGuid(),
                Amount = amount,
            });
        public void ChangeOutgo(Guid id, decimal amount)
                => Apply(new Events.OutgoAmountChanged
                {
                    DailyBudgetId = Id,
                    OutgoId = id,
                    Amount = amount
                });
        public void RemoveOutgo(Guid outgoId)
            => Apply(new Events.OutgoRemoved
            {
                DailyBudgetId = Id,
                OutgoId = outgoId
            });
        public void SetPeriod(DateTime start, DateTime end)
            => Apply(new Events.PeriodAddedToDailyBudget
            {
                DailyBudgetId = Id,
                PeriodId = Guid.NewGuid(),
                Start = start,
                End = end
            });
        public void ChangeStart(DateTime start)
            => Apply(new Events.PeriodStartChanged
            {
                Start = start
            });
        public void ChangeEnd(DateTime end)
            => Apply(new Events.PeriodEndChanged 
            {
                End = end
            });
    
        protected override void When(object @event)
        {
            Income income;
            Outgo outgo;
            Period period;

            switch(@event)
            {
                case Events.DailyBudgetCreated e:
                    Id = new DailyBudgetId(e.Id);
                    break;
                case Events.IncomeAddedToDailyBudget e:
                    income = new Income(Apply);
                    ApplyToEntity(income, e);
                    Incomes.Add(income);
                    DailyAmount = DailyAmount.FromDecimal(e.DailyAmount);
                    break;
                case Events.IncomeRemoved e:
                    income = Incomes.FirstOrDefault(i => i.Id == e.IncomeId);
                    if (income == null)
                        throw new InvalidOperationException($"Income with id {e.IncomeId} not found");
                    Incomes.Remove(income);
                    break;
                case Events.IncomeAmountChanged e:
                    income = Incomes.FirstOrDefault(i => i.Id == e.IncomeId);
                    if (income == null)
                        throw new InvalidOperationException($"Income with id {e.IncomeId} not found");
                    ApplyToEntity(income, e);
                    break;
                case Events.OutgoAddedToDailyBudget e:
                    outgo = new Outgo(Apply);
                    ApplyToEntity(outgo, e);
                    Outgos.Add(outgo);
                    break;
                case Events.OutgoRemoved e:
                    outgo = Outgos.FirstOrDefault(o => o.Id == e.OutgoId);
                    if (outgo == null)
                        throw new InvalidOperationException($"Outgo with id {e.OutgoId} not found");
                    Outgos.Remove(outgo);
                    break;
                case Events.OutgoAmountChanged e:
                    outgo = Outgos.FirstOrDefault(i => i.Id == e.OutgoId);
                    if (outgo == null)
                        throw new InvalidOperationException($"Outgo with id {e.OutgoId} not found");
                    ApplyToEntity(outgo, e);
                    break;
                case Events.PeriodAddedToDailyBudget e:
                    period = new Period(Apply);
                    ApplyToEntity(period,e);
                    Period = period;
                    break;
                case Events.PeriodStartChanged e:
                    if (Period == null)
                        throw new InvalidOperationException("Period is not set");
                    ApplyToEntity(Period, e);
                    break;
                case Events.PeriodEndChanged e:
                    if (Period == null)
                        throw new InvalidOperationException("Period is not set");
                    ApplyToEntity(Period, e);
                    break;
            }   
        }
        protected override void EnsureValidState()
        {}

        public Amount TotalIncome => Amount.FromDecimal(Incomes.Sum(i => i.Amount));
        public Amount TotalOutgo => Amount.FromDecimal(Outgos.Sum(i => i.Amount));
        public List<Income> Incomes { get; }
        public List<Outgo> Outgos{ get; }
        public DailyAmount DailyAmount { get; private set; }
        public Period Period { get; set; }
        private decimal CalculateAvailable(decimal amount)
        {
            var totalIncome = TotalIncome + amount;
            return totalIncome / Period.Days;
        }
    }
}
