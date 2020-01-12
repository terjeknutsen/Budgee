using Budgee.Domain.DailyBudgets;
using Budgee.Projections;
using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Budgee.Infrastucture
{
    public sealed class EsSubscription
    {
        private readonly IEventStoreConnection connection;
        private readonly IList<ReadModels.DailyBudgets> items;
        private EventStoreAllCatchUpSubscription subscription;

        public EsSubscription(IEventStoreConnection connection,
            IList<ReadModels.DailyBudgets> items)
        {
            this.connection = connection;
            this.items = items;
        }
        public void Start()
        {
            var settings = new CatchUpSubscriptionSettings(2000, 500, false, true, "try-out-subscription");
            subscription = connection.SubscribeToAllFrom(Position.Start, settings, EventAppeared);
        }

        private Task EventAppeared(EventStoreCatchUpSubscription subscription, ResolvedEvent resolvedEvent)
        {
            if (resolvedEvent.Event.EventType.StartsWith("$")) return Task.CompletedTask;
            var @event = resolvedEvent.Deserialize();
            switch(@event)
            {
                case Events.DailyBudgetCreated e:
                    items.Add(new ReadModels.DailyBudgets
                    {
                        DailyBudgtId = e.Id
                    });
                    break;
                
            }
            return Task.CompletedTask;
        }
    }
}
