﻿namespace Budgee.Domain.Shared
{
    public interface ICurrencyLookup
    {
        Currency FindCurrency(string currencyCode);
    }

    public sealed class Currency
    {
        public string  CurrencyCode { get; set; }
        public bool InUse { get; set; }
        public int DecimalPlaces { get; set; }

        public static Currency None = new Currency { InUse = false };
    }
}
