using CareerIntelligencePlatform.Domain.Exceptions;
using CareerIntelligencePlatform.Domain.ValueObjects;

namespace CareerIntelligencePlatform.Domain.Tests;

public class MoneyTests
{
  [Fact]
  public void Create_ShouldCreateMoney_WhenDataIsValid()
  {
    // Arrange
    const decimal amount = 1000m;
    const string currency = "USD";

    // Act
    var money = Money.Create(amount, currency);

    // Assert
    Assert.Equal(amount, money.Amount);
    Assert.Equal(currency, money.Currency);
  }

  [Fact]
  public void Create_ShouldThrow_WhenAmountIsNegative()
  {
    // Arrange
    const decimal amount = -1m;
    const string currency = "USD";

    // Act & Assert
    var exception = Assert.Throws<DomainException>(
        () => Money.Create(amount, currency));

    Assert.Equal(
    "MONEY_AMOUNT_NEGATIVE",
    exception.Code);

    Assert.Equal(
        "Amount cannot be negative.",
        exception.Message);
  }

  [Theory]
  [InlineData("")]
  [InlineData(" ")]
  public void Create_ShouldThrow_WhenCurrencyIsInvalid(string currency)
  {
    // Arrange
    const decimal amount = 1000m;

    // Act & Assert
    var exception = Assert.Throws<DomainException>(
        () => Money.Create(amount, currency));

    Assert.Equal(
        "Currency is required.",
        exception.Message);
  }

  [Fact]
  public void TwoMoneyObjects_ShouldBeEqual_WhenTheirValuesAreEqual()
  {
    // Arrange
    var first = Money.Create(1000m, "USD");
    var second = Money.Create(1000m, "USD");

    // Act & Assert
    Assert.Equal(first, second);
  }

  [Fact]
  public void TwoMoneyObjects_ShouldNotBeEqual_WhenTheirValuesAreDifferent()
  {
    // Arrange
    var first = Money.Create(1000m, "USD");
    var second = Money.Create(2000m, "USD");

    // Act & Assert
    Assert.NotEqual(first, second);
  }

  [Fact]
  public void TwoMoneyObjects_ShouldNotBeEqual_WhenTheirCurrenciesAreDifferent()
  {
    // Arrange
    var first = Money.Create(1000m, "USD");
    var second = Money.Create(1000m, "EUR");

    // Act & Assert
    Assert.NotEqual(first, second);
  }
}