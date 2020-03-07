using Budgee.Framework;
using System;

namespace Budgee.Domain.DailyBudgets
{
    public sealed class BudgetName : Value<BudgetName>
    {
        readonly string name;
        public BudgetName(string name) 
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException($"{nameof(BudgetName)} cannot be empty");
            if (name.Length > 50)
                throw new ArgumentOutOfRangeException($"{nameof(BudgetName)} lenght cannot exceed 50 chars");
            this.name = name; 
        }
        protected override bool CompareProperties(BudgetName other)
            => name == other.name;
        public static implicit operator string(BudgetName self)=> self.name;
        public static implicit operator BudgetName(string name)=> new BudgetName(name);
    }
}
