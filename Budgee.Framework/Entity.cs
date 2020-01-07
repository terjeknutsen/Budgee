using System;

namespace Budgee.Framework
{
    public abstract class Entity<TId> : IInternalEventHandler where TId : Value<TId>
    {
        private readonly Action<object> applier;
        public TId Id{ get; protected set; }
        protected Entity(Action<object> applier) => this.applier = applier;
        protected abstract void When(object @event);
        protected void Apply(object @event)
        {
            When(@event);
            applier(@event);
        }
        void IInternalEventHandler.Handle(object @event) => When(@event);
    }
}
