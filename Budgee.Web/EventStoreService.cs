using Budgee.Infrastructure;
using EventStore.ClientAPI;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Budgee
{
    public class EventStoreService : IHostedService
    {
        private readonly IEventStoreConnection connection;
        private readonly EsSubscription subscription;
        public EventStoreService(IEventStoreConnection connection,
        EsSubscription subscription)
        {
            this.connection = connection;
            this.subscription = subscription;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await connection.ConnectAsync();
            subscription.Start();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            subscription.Stop();
            connection.Close();
            return Task.CompletedTask;
        }
    }
}
