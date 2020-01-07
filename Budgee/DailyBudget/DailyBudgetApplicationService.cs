using Budgee.Domain.DailyBudget;
using Budgee.Framework;
using System;
using System.Threading.Tasks;
using static Budgee.DailyBudget.Commands;

namespace Budgee.DailyBudget
{
    public sealed class DailyBudgetApplicationService : IApplicationService
    {
        private readonly IDailyBudgetRepository repository;

        public DailyBudgetApplicationService(IDailyBudgetRepository repository)
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
                       b => b.AddIncome(cmd.Amount)),
                V1.AddOutgo cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.AddOutgo(cmd.Amount)),
                V1.SetPeriod cmd =>
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.SetPeriod(cmd.Start,cmd.End)),
                V1.ChangeIncome cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeIncome(cmd.IncomeId,cmd.Amount)),
                V1.ChangeOutgo cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeOutgo(cmd.OutgoId,cmd.Amount)),
                V1.ChangeStart cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeStart(cmd.Start)),
                V1.ChangeEnd cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.ChangeEnd(cmd.End)),
                V1.RemoveIncome cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.RemoveIncome(cmd.IncomeId)),
                V1.RemoveOuto cmd => 
                    HandleUpdate(
                        cmd.DailyBudgetId,
                        b => b.RemoveOutgo(cmd.OutgoId)),
                _ => Task.CompletedTask

            };

        private async Task HandleCreate(V1.Create cmd)
        {
            if (await repository.Exists(cmd.DailyBudgetId.ToString()))
                throw new InvalidOperationException(
                    $"Entity with id {cmd.DailyBudgetId} already exists"
                );

            var dailyBudget =
                new Domain.DailyBudget.DailyBudget(
                    new DailyBudgetId(cmd.DailyBudgetId));
            await repository.Add(dailyBudget);

        }

        private async Task HandleUpdate(Guid dailyBudgetId, Action<Domain.DailyBudget.DailyBudget> operation){
            var dailyBudget = await repository.Load(dailyBudgetId.ToString());
            if (dailyBudget == null)
                throw new InvalidOperationException(
                    $"Entity with id {dailyBudgetId} cannot be found");
            operation(dailyBudget);
        }
    }
}
