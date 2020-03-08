using Budgee.DailyBudgets.Messages;
using Budgee.DailyBudgets.Messages.DailyBudgets;
using EventStore.ClientAPI;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILogger = Serilog.ILogger;

namespace Budgee.Infrastructure
{
    public class EsSubscription
    {
        private static readonly ILogger log = Log.ForContext<EsSubscription>();
        private readonly IEventStoreConnection connection;
        private readonly IList<ReadModels.DailyBudgets> items;
        private EventStoreAllCatchUpSubscription subscription;
        public EsSubscription(IEventStoreConnection connection, 
            IList<ReadModels.DailyBudgets> items)
        {
            this.connection = connection;
            this.items = items;
        }

        public void Start(){
            var settings = new CatchUpSubscriptionSettings(2000, 500, Log.IsEnabled(Serilog.Events.LogEventLevel.Verbose), true, "try-out-subscription");
            subscription = connection.SubscribeToAllFrom(Position.Start, settings, EventAppeared);
        }
        private Task EventAppeared(EventStoreCatchUpSubscription subscription,ResolvedEvent resolveEvent){
            if (resolveEvent.Event.EventType.StartsWith("$"))
                return Task.CompletedTask;
            var @event = resolveEvent.Deserialize();
            log.Debug("Projecting event {type}", @event.GetType().Name);

            switch(@event)
            {
                case Events.DailyBudgetCreated e:
                    items.Add(new ReadModels.DailyBudgets
                    {
                        DailyBudgtId = e.Id,
                        Name = e.Name,
                        Snapshot = new ReadModels.Snapshots { DailyBudgetId = e.Id },
                        Expenditures = new List<ReadModels.Expenditures>(),
                        Incomes = new List<ReadModels.Incomes>(),
                        Outgos = new List<ReadModels.Outgos>()
                    });
                    break;
                case Events.IncomeAddedToDailyBudget e:
                    UpdateItem(e.DailyBudgetId, budget =>
                        {
                            budget.Incomes.Add(new ReadModels.Incomes { DailyBudgetId = e.DailyBudgetId, IncomeId = e.IncomeId, Amount = e.Amount, Description = e.Description });
                        });
                    break;
                case Events.OutgoAddedToDailyBudget e:
                    UpdateItem(e.DailyBudgetId, budget =>
                        {
                            budget.Outgos.Add(new ReadModels.Outgos { DailyBudgetId = e.DailyBudgetId, OutgoId = e.OutgoId, Amount = e.Amount, Description = e.Description });

                        });
                    break;
                case Events.ExpenditureAdded e:
                    UpdateItem(e.DailyBudgetId, budget =>
                       {
                           budget.Expenditures.Add(new ReadModels.Expenditures { DailyBudgetId = e.DailyBudgetId, ExpenditureId = e.ExpenditureId, Amount = e.Amount, Description = e.Description });
                       });
                    break;
                case Events.IncomeAmountChanged e:
                    UpdateItem(e.DailyBudgetId, budget =>
                    {
                        var income = budget.Incomes.FirstOrDefault(i => i.IncomeId == e.IncomeId);
                        if(income!=null)
                        {
                            budget.Incomes.Remove(income);
                            income.Amount = e.Amount;
                            budget.Incomes.Add(income);
                        }
                    });
                    break;
                case Events.IncomeDescriptionChanged e:
                    UpdateItem(e.DailyBudgetId, budget =>
                    {
                        var income = budget.Incomes.FirstOrDefault(i => i.IncomeId == e.IncomeId);
                        if(income!=null)
                        {
                            budget.Incomes.Remove(income);
                            income.Description = e.Description;
                            budget.Incomes.Add(income);
                        }
                    });
                    break;
                case Events.OutgoAmountChanged e:
                    UpdateItem(e.DailyBudgetId, budget =>
                    {
                        var outgo = budget.Outgos.FirstOrDefault(i => i.OutgoId == e.OutgoId);
                        if (outgo != null)
                        {
                            budget.Outgos.Remove(outgo);
                            outgo.Amount = e.Amount;
                            budget.Outgos.Add(outgo);
                        }
                    });
                    break;
                case Events.OutgoDescriptionChanged e:
                    UpdateItem(e.DailyBudgetId, budget =>
                    {
                        var outgo = budget.Outgos.FirstOrDefault(i => i.OutgoId == e.OutgoId);
                        if (outgo != null)
                        {
                            budget.Outgos.Remove(outgo);
                            outgo.Description = e.Description;
                            budget.Outgos.Add(outgo);
                        }
                    });
                    break;
                case Events.ExpenditureAmountChanged e:
                    UpdateItem(e.DailyBudgetId, budget =>
                    {
                        var outgo = budget.Expenditures.FirstOrDefault(i => i.ExpenditureId == e.ExpenditureId);
                        if (outgo != null)
                        {
                            budget.Expenditures.Remove(outgo);
                            outgo.Amount = e.Amount;
                            budget.Expenditures.Add(outgo);
                        }
                    });
                    break;
                case Events.ExpenditureDescriptionChanged e:
                    UpdateItem(e.DailyBudgetId, budget =>
                    {
                        var outgo = budget.Expenditures.FirstOrDefault(i => i.ExpenditureId == e.ExpenditureId);
                        if (outgo != null)
                        {
                            budget.Expenditures.Remove(outgo);
                            outgo.Description = e.Description;
                            budget.Expenditures.Add(outgo);
                        }
                    });
                    break;
                case Events.SnapshotChanged e:
                    UpdateItem(e.DailyBudgetId, budget => {
                        budget.Snapshot.Daily = e.Daily;
                        budget.Snapshot.Available = e.Available;
                        budget.Snapshot.SnapshotId = e.SnapshotId;
                    });
                    break;
                case Events.PeriodAddedToDailyBudget e:
                    UpdateItem(e.DailyBudgetId, budget =>
                    {
                        budget.Start = e.Start;
                        budget.End = e.End;
                    });
                    break;
                case Events.PeriodStartChanged e:
                    UpdateItem(e.DailyBudgetId, budget => budget.Start = e.Start);
                    break;
                case Events.PeriodEndChanged e:
                    UpdateItem(e.DailyBudgetId, budget => budget.End = e.End);
                    break;
            }
            return Task.CompletedTask;
        }

        private void UpdateItem(Guid id, Action<ReadModels.DailyBudgets> update){
            var item = items.FirstOrDefault(x => x.DailyBudgtId == id);
            if (item == null) return;
            update(item);
        }
        public void Stop() => subscription.Stop();
    }
}
