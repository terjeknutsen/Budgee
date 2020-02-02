using Budgee.Domain.DailyBudgets;
using Budgee.Framework;
using System;
using System.Threading.Tasks;
using static Budgee.DailyBudgets.Messages.DailyBudgets.Commands;

namespace Budgee.DailyBudgets
{
    public sealed class DailyBudgetApplicationService : IApplicationService
    {
        private readonly IDailyBudgetRepository repository;

        public DailyBudgetApplicationService(
        IDailyBudgetRepository repository)
        {
            this.repository = repository;
        }
        public Task Handle(object command)
            => command switch
            {
                V1.Create cmd =>
                    HandleCreate(cmd),
                V1.AddIncome cmd =>
                    HandleUpdate(
                       cmd.DailyBudgetId,
                       b => b.AddIncome(cmd.Amount,DateTime.Now)),
                V1.AddOutgo cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.AddOutgo(cmd.Amount, DateTime.Now)),
                V1.SetPeriod cmd =>
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.SetPeriod(cmd.Start,cmd.End)),
                V1.ChangeIncome cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeIncome(cmd.IncomeId,cmd.Amount, DateTime.Now)),
                V1.ChangeOutgo cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeOutgo(cmd.OutgoId,cmd.Amount, DateTime.Now)),
                V1.ChangeStart cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeStart(cmd.Start,DateTime.Now)),
                V1.ChangeEnd cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeEnd(cmd.End,DateTime.Now)),
                V1.RemoveIncome cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.RemoveIncome(cmd.IncomeId, DateTime.Now)),
                V1.RemoveOuto cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.RemoveOutgo(cmd.OutgoId, DateTime.Now)),
                _ => Task.CompletedTask

            };

        private async Task HandleCreate(V1.Create cmd)
        {
            if (await repository.Exists(cmd.DailyBudgetId.ToString()))
                throw new InvalidOperationException(
                    $"Entity with id {cmd.DailyBudgetId} already exists"
                );

            var dailyBudget =
                new DailyBudget(
                    new DailyBudgetId(cmd.DailyBudgetId));
            await repository.Add(dailyBudget);

        }

        private async Task HandleUpdate(Guid dailyBudgetId, Action<DailyBudget> operation){
            var dailyBudget = await repository.Load(dailyBudgetId.ToString());
            if (dailyBudget == null)
                throw new InvalidOperationException(
                    $"Entity with id {dailyBudgetId} cannot be found");
            operation(dailyBudget);
            await repository.Update(dailyBudget);
            
        }
    }
}
