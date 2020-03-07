using EventStore.ClientAPI;
using System;
using System.Text;
using System.Text.Json;

namespace Budgee.Infrastructure
{
    public static class EventDeserializer
    {
        public static object Deserialize(this ResolvedEvent resolvedEvent)
        {
            var meta = JsonSerializer.Deserialize<EventMetadata>(Encoding.UTF8.GetString(resolvedEvent.Event.Metadata));
            var dataType = Type.GetType(meta.ClrType);
            var jsonData = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
            var data = JsonSerializer.Deserialize(jsonData, dataType);
            return data;
        }
    }
}
