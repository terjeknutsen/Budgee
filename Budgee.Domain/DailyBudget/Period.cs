using Budgee.Framework;
using System;

namespace Budgee.Domain.DailyBudget
{
    public sealed class Period : Entity<PeriodId>
    {
        public Period(Action<object> applier) : base(applier)
        {}

        public DailyBudgetId ParentId{ get; private set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Days { get; set; }

        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.PeriodAddedToDailyBudget e:
                    if (e.Start > e.End)
                        throw new ArgumentOutOfRangeException($"Start: {e.Start} cannot come after end: {e.End}");
                    ParentId = new DailyBudgetId(e.DailyBudgetId);
                    Id = new PeriodId(e.PeriodId);
                    StartTime = e.Start;
                    EndTime = e.End;
                    Days = (int)(EndTime - StartTime).TotalDays;
                    break;
                case Events.PeriodStartChanged e:
                    if (e.Start > EndTime)
                        throw new ArgumentOutOfRangeException($"Start: {e.Start} cannot come after end: {EndTime}");
                    StartTime = e.Start;
                    break;
                case Events.PeriodEndChanged e:
                    if (StartTime > e.End)
                        throw new ArgumentOutOfRangeException($"End: {e.End} cannot come before start: {StartTime}");
                    EndTime = e.End;
                    break;
            }
        }
    }

    public class PeriodId : Value<PeriodId>
    {
        private readonly Guid value;

        public PeriodId(Guid value) => this.value = value;

        protected override bool CompareProperties(PeriodId other)
            => value == other.value;
        public static implicit operator Guid(PeriodId self) => self.value;
    }
}
