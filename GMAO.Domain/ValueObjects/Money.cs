using GMAO.Domain.Common;
using GMAO.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.ValueObjects
{
    public class Money : ValueObject
    {
        public decimal Amount { get; init; }
        public string Currency { get; init; } = "EUR";

        public Money(decimal amount, string currency = "EUR")
        {
            Amount = amount;
            Currency = currency;
        }

        public static Money Zero(string currency = "EUR") => new Money(0, currency);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationDomainException("Cannot add money with different currencies");
            return new Money(a.Amount + b.Amount, a.Currency);
        }

        public static Money operator -(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationDomainException("Cannot subtract money with different currencies");
            return new Money(a.Amount - b.Amount, a.Currency);
        }

        public static Money operator *(Money a, decimal multiplier)
        {
            return new Money(a.Amount * multiplier, a.Currency);
        }
    }

}
