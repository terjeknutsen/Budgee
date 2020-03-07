using Budgee.Framework;
using System;

namespace Budgee.Domain.Shared
{
    public class Money : Value<Money>
    {
        protected Money(decimal amount)
        {
            if (decimal.Round(amount, 2) != amount)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot have more than 2 decimals");
            Amount = amount;
        }

        public decimal Amount { get; }

        public Money Add(Money summand) 
        {
           return new Money(Amount + summand.Amount);
        }
        public Money Subtract(Money subtrahend) 
        {
            return new Money(Amount - subtrahend.Amount); 
         }
        public static Money operator +(Money summand1, Money summand2) => summand1.Add(summand2);
        public static Money operator -(Money minuend, Money subtrahend) => minuend.Subtract(subtrahend);
 
        protected override bool CompareProperties(Money other)
        {
            return Amount.Equals(other.Amount);
        }

        public override string ToString() => $"{Amount},-";

        public static Money FromDecimal(decimal amount) => new Money(amount);
        public static Money FromString(string amount) => new Money(decimal.Parse(amount));
    }
}
