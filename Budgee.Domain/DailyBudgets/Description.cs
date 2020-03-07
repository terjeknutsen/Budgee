using Budgee.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budgee.DailyBudgets.Domain.DailyBudgets
{
    public sealed class Description : Value<Description>
    {
        readonly string description;
        public Description(string description)
        {
            if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
                throw new InvalidOperationException($"{nameof(Description)} cannot be empty");
            if (description.Length > 250)
                throw new ArgumentOutOfRangeException($"{nameof(Description)} lenght cannot exceed 250 chars");
            this.description = description;
        }
        protected override bool CompareProperties(Description other)
        => description == other.description;
        public static implicit operator string(Description self) => self.description;
        public static implicit operator Description(string description) => new Description(description);
    }
}
