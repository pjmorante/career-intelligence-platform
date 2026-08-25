using CareerIntelligencePlatform.Domain.Exceptions;

namespace CareerIntelligencePlatform.Domain.ValueObjects;

public sealed record Money
{
  public decimal Amount { get; }
  public string Currency { get; }

  private Money(decimal amount, string currency)
  {
    Amount = amount;
    Currency = currency;
  }

  public static Money Create(decimal amount, string currency)
  {
    if (amount < 0)
    {
      throw new DomainException(
          DomainErrorCodes.MoneyAmountNegative,
          "Amount cannot be negative.");
    }

    if (string.IsNullOrWhiteSpace(currency))
    {
      throw new DomainException(
          DomainErrorCodes.MoneyCurrencyRequired,
          "Currency is required.");
    }

    return new Money(amount, currency);
  }
}