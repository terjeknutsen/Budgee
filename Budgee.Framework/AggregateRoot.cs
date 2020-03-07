using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Budgee.Framework
{
    public abstract class AggregateRoot<TId> : IInternalEventHandler 
    {
        private readonly List<object> changes;
        protected AggregateRoot() => changes = new List<object>();
        protected abstract void When(object @event);
        protected abstract void EnsureValidState();
        protected void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            changes.Add(@event);
        }
        protected void ApplyToEntity(IInternalEventHandler entity, object @event)
            => entity?.Handle(@event);
        public void Load(IEnumerable<object> history)
        {
            foreach(var e in history)
            {
                When(e);
                Version++;
            }
        }
        public TId Id{ get; protected set; }
        public int Version { get; private set; } = -1;
        public IEnumerable<object> GetChanges() => changes.AsEnumerable();
        public void ClearChanges() => changes.Clear();
        void IInternalEventHandler.Handle(object @event) => When(@event);
    }
}
