using Budgee.Domain.DailyBudgets;
using Budgee.Framework;
using System;
using System.Threading.Tasks;
using static Budgee.DailyBudgets.Messages.DailyBudgets.Commands;

namespace Budgee.DailyBudgets
{
    public sealed class DailyBudgetsCommandService : IApplicationService
    {
        private readonly IAggregateStore aggregateStore;

        public DailyBudgetsCommandService(
        IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }
        public Task Handle(object command)
            => command switch
            {
                V1.Create cmd =>
                    HandleCreate(cmd),
                V1.AddIncome cmd =>
                    HandleUpdate(
                       cmd.DailyBudgetId,
                       b => b.AddIncome(cmd.Amount, cmd.Description, DateTime.Now)),
                V1.AddOutgo cmd =>
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.AddOutgo(cmd.Amount, cmd.Description, DateTime.Now)),
                V1.AddExpenditure cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b=> b.AddExpenditure(cmd.Amount,cmd.Description,DateTime.Now)),
                V1.SetPeriod cmd =>
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.SetPeriod(cmd.Start, cmd.End)),
                V1.ChangeIncomeAmount cmd =>
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeIncome(cmd.IncomeId, cmd.Amount, DateTime.Now)),
                V1.ChangeOutgoAmount cmd =>
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeOutgo(cmd.OutgoId, cmd.Amount, DateTime.Now)),
                V1.ChangeExpenditureAmount cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeExpenditure(cmd.ExpenditureId, cmd.Amount,DateTime.Now)),
                V1.ChangeIncomeDescription cmd =>
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeIncome(cmd.IncomeId,cmd.Description,DateTime.Now)),
                V1.ChangeOutgoDescription cmd=>
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeOutgo(cmd.OutgoId, cmd.Description,DateTime.Now)),
                V1.ChangeExpenditureDescription cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeExpenditure(cmd.ExpenditureId,cmd.Description,DateTime.Now)),
                V1.ChangeStart cmd =>
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeStart(cmd.Start, DateTime.Now)),
                V1.ChangeEnd cmd =>
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeEnd(cmd.End, DateTime.Now)),
                V1.RemoveIncome cmd =>
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.RemoveIncome(cmd.IncomeId, DateTime.Now)),
                V1.RemoveOuto cmd =>
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.RemoveOutgo(cmd.OutgoId, DateTime.Now)),
                V1.RemoveExpenditure cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.RemoveExpenditure(cmd.ExpenditureId, DateTime.Now)),
                _ => Task.CompletedTask

            };

        private async Task HandleCreate(V1.Create cmd)
        {
            if (await aggregateStore.Exists<DailyBudget, DailyBudgetId>(cmd.DailyBudgetId.ToString()))
                throw new InvalidOperationException(
                    $"Entity with id {cmd.DailyBudgetId} already exists"
                );

            var dailyBudget =
                new DailyBudget(
                    new DailyBudgetId(cmd.DailyBudgetId),
                    new BudgetName(cmd.BudgetName));
            dailyBudget.SetPeriod(cmd.Start, cmd.End);
            dailyBudget.AddIncome(cmd.Income, "IN", DateTime.Now);
            dailyBudget.AddOutgo(cmd.Outgo, "OUT", DateTime.Now);
            await aggregateStore.Save<DailyBudget, DailyBudgetId>(dailyBudget);
        }
        private Task HandleUpdate(Guid id, Action<DailyBudget> update)
            => this.HandleUpdate(aggregateStore, new DailyBudgetId(id), update);
    }
}
