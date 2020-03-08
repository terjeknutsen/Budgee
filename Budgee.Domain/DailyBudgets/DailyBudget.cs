using Budgee.DailyBudgets.Messages.DailyBudgets;
using Budgee.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Budgee.Domain.DailyBudgets
{
    public class DailyBudget : AggregateRoot<DailyBudgetId>
    {
        protected DailyBudget(){ }
        public DailyBudget(DailyBudgetId id,BudgetName name)
        {
            Apply(new Events.DailyBudgetCreated 
            { 
                Id = id,
                Name = name
            });
        }
        public void AddIncome(decimal amount,string description, DateTime entryDate)
            => Apply(new Events.IncomeAddedToDailyBudget
                {
                    DailyBudgetId = Id,
                    IncomeId = Guid.NewGuid(),
                    Amount = amount,
                    EntryDate = entryDate,
                    Description = description
                });
   


        public void ChangeIncome(Guid incomeId, decimal amount,DateTime entryDate)
        =>
            Apply(new Events.IncomeAmountChanged
            {
                DailyBudgetId = Id,
                IncomeId = incomeId,
                Amount = amount,
                EntryDate = entryDate
            });
        public void ChangeIncome(Guid incomeId, string description, DateTime entryDate)
            => Apply(new Events.IncomeDescriptionChanged
            {
                DailyBudgetId = Id,
                IncomeId = incomeId,
                Description = description,
                EntryDate = entryDate
            });
      
        public void RemoveIncome(Guid incomeId, DateTime entryDate)
         =>   Apply(new Events.IncomeRemoved
            {
                DailyBudgetId = Id,
                IncomeId = incomeId,
                EntryDate = entryDate,
            });
        public void AddOutgo(decimal amount,string description, DateTime entryDate)
         =>   Apply(new Events.OutgoAddedToDailyBudget
            {
                DailyBudgetId = Id,
                OutgoId = Guid.NewGuid(),
                Amount = amount,
                EntryDate = entryDate,
                Description = description
            });
        public void ChangeOutgo(Guid id, decimal amount, DateTime entryDate)
            => Apply(new Events.OutgoAmountChanged
            {
               DailyBudgetId = Id,
               OutgoId = id,
               Amount = amount,
               EntryDate = entryDate
            });
        public void ChangeOutgo(Guid id, string description, DateTime entryDate)
            => Apply(new Events.OutgoDescriptionChanged
            {
                DailyBudgetId = Id,
                OutgoId = id,
                Description = description,
                EntryDate = entryDate
            });
        public void RemoveOutgo(Guid outgoId, DateTime entryDate)
            => Apply(new Events.OutgoRemoved
            {
             DailyBudgetId = Id,
             OutgoId = outgoId,
             EntryDate = entryDate
            });

        public void AddExpenditure(decimal amount,string description, DateTime entryDate)
         =>   Apply(new Events.ExpenditureAdded
            {
                DailyBudgetId = Id,
                ExpenditureId = Guid.NewGuid(),
                Amount = amount,
                Description = description,
                EntryDate = entryDate
            });

        public void ChangeExpenditure(Guid id, decimal amount, DateTime entryDate)
            => Apply(new Events.ExpenditureAmountChanged
            {
                DailyBudgetId = Id,
                ExpenditureId = id,
                Amount = amount,
                EntryDate = entryDate
            });
        public void ChangeExpenditure(Guid id, string description, DateTime entryDate)
            => Apply(new Events.ExpenditureDescriptionChanged
            {
                DailyBudgetId = Id,
                ExpenditureId = id,
                Description = description,
                EntryDate = entryDate
            });
        public void RemoveExpenditure(Guid id, DateTime entryDate)
            => Apply(new Events.ExpenditureRemoved
            {
                DailyBudgetId = Id,
                ExpenditureId = id,
                EntryDate = entryDate
            });
        public void SetPeriod(DateTime start, DateTime end)
         =>   Apply(new Events.PeriodAddedToDailyBudget
            {
                DailyBudgetId = Id,
                PeriodId = Guid.NewGuid(),
                Start = start,
                End = end
            });
         
        public void ChangeStart(DateTime start,DateTime entryDate)
         =>   Apply(new Events.PeriodStartChanged
            {
                DailyBudgetId = Id,
                Start = start,
                EntryDate = entryDate
            });
    
        public void ChangeEnd(DateTime end,DateTime entryDate)
         =>   Apply(new Events.PeriodEndChanged
            {
                DailyBudgetId = Id,
                End = end,
                EntryDate = entryDate
            });
          
        
    
        protected override void When(object @event)
        {
            Income income;
            Outgo outgo;
            Snapshot snapshot;
            Expenditure expenditure;
            switch(@event)
            {
                case Events.DailyBudgetCreated e:
                    Id = new DailyBudgetId(e.Id);
                    Name = new BudgetName(e.Name);
                    snapshot = new Snapshot(Apply);
                    ApplyToEntity(snapshot, e);
                    Snapshot = snapshot;
                    break;
                case Events.IncomeAddedToDailyBudget e:
                    income = new Income(Apply);
                    ApplyToEntity(income, e);
                    Incomes.Add(income);
                    e.TotalIncome = TotalIncome();
                    ApplyToEntity(Snapshot, e);
                    break;
                case Events.IncomeAmountChanged e:
                    income = Incomes.FirstOrDefault(i => i.Id == e.IncomeId);
                    if (income == null)
                        throw new InvalidOperationException($"Income with id {e.IncomeId} not found");
                    ApplyToEntity(income, e);
                    e.TotalIncome = TotalIncome();
                    ApplyToEntity(Snapshot, e);
                    break;
                case Events.IncomeDescriptionChanged e:
                    income = Incomes.FirstOrDefault(i => i.Id == e.IncomeId);
                    if (income == null)
                        throw new InvalidOperationException($"Income with id {e.IncomeId} not found");
                    ApplyToEntity(income, e);
                    break;
                case Events.IncomeRemoved e:
                    income = Incomes.FirstOrDefault(i => i.Id == e.IncomeId);
                    if (income == null)
                        throw new InvalidOperationException($"Income with id {e.IncomeId} not found");
                    Incomes.Remove(income);
                    e.TotalIncome = TotalIncome();
                    ApplyToEntity(Snapshot, e);
                    break;
                case Events.OutgoAddedToDailyBudget e:
                    outgo = new Outgo(Apply);
                    ApplyToEntity(outgo, e);
                    Outgos.Add(outgo);
                    e.TotalOutgo = TotalOutgo();
                    ApplyToEntity(Snapshot, e);
                    break;
                case Events.OutgoAmountChanged e:
                    outgo = Outgos.FirstOrDefault(i => i.Id == e.OutgoId);
                    if (outgo == null)
                        throw new InvalidOperationException($"Outgo with id {e.OutgoId} not found");
                    ApplyToEntity(outgo, e);
                    e.TotalOutgo = TotalOutgo();
                    ApplyToEntity(Snapshot, e);
                    break;
                case Events.OutgoDescriptionChanged e:
                    outgo = Outgos.FirstOrDefault(i => i.Id == e.OutgoId);
                    if (outgo == null)
                        throw new InvalidOperationException($"Outgo with id {e.OutgoId} not found");
                    ApplyToEntity(outgo, e);
                    break;
                case Events.OutgoRemoved e:
                    outgo = Outgos.FirstOrDefault(o => o.Id == e.OutgoId);
                    if (outgo == null)
                        throw new InvalidOperationException($"Outgo with id {e.OutgoId} not found");
                    Outgos.Remove(outgo);
                    e.TotalOutgo = TotalOutgo();
                    ApplyToEntity(Snapshot, e);
                    break;
                case Events.ExpenditureAdded e:
                    expenditure = new Expenditure(Apply);
                    ApplyToEntity(expenditure, e);
                    Expenditures.Add(expenditure);
                    e.TotalExpenditure = TotalExpenditure();
                    ApplyToEntity(Snapshot, e);
                    break;
                case Events.ExpenditureAmountChanged e:
                    expenditure = Expenditures.FirstOrDefault(exp => exp.Id == e.ExpenditureId);
                    if (expenditure == null)
                        throw new InvalidOperationException($"Expenditure with id {e.ExpenditureId} not found");
                    ApplyToEntity(expenditure, e);
                    e.TotalExpenditure = TotalExpenditure();
                    ApplyToEntity(Snapshot, e);
                    break;
                case Events.PeriodAddedToDailyBudget e:
                    if (Period != null)
                        throw new InvalidOperationException($"Period has already been set. Update start or end");
                    Period = Period.Create(e.Start, e.End);
                    ApplyToEntity(Snapshot, e);
                    break;
                case Events.PeriodStartChanged e:
                    Period = Period.Create(e.Start, Period.ToB);
                    ApplyToEntity(Snapshot, e);
                    break;
                case Events.PeriodEndChanged e:
                    Period = Period.Create(Period.FromA, e.End);
                    ApplyToEntity(Snapshot, e);
                    break;
       
            }   
        }
        protected override void EnsureValidState()
        {
            
        }

        private Amount TotalIncome() => Amount.FromDecimal(Incomes.Sum(i => i.Amount));
        private Amount TotalOutgo() => Amount.FromDecimal(Outgos.Sum(i => i.Amount));
        private Amount TotalExpenditure() => Amount.FromDecimal(Expenditures.Sum(i => i.Amount));
        public List<Income> Incomes { get; } = new List<Income>();
        public List<Outgo> Outgos { get; } = new List<Outgo>();
        public List<Expenditure> Expenditures { get; } = new List<Expenditure>();
        public Period Period { get; private set; }
        public Snapshot Snapshot{ get; private set; }
        public BudgetName Name{ get; private set; }
    }
}
