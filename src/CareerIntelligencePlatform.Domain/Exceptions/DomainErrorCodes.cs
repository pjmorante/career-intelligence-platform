namespace CareerIntelligencePlatform.Domain.Exceptions;

public static class DomainErrorCodes
{
  public const string JobTitleRequired = "JOB_TITLE_REQUIRED";
  public const string JobTitleTooLong = "JOB_TITLE_TOO_LONG";

  public const string JobDescriptionRequired = "JOB_DESCRIPTION_REQUIRED";
  public const string JobDescriptionTooLong = "JOB_DESCRIPTION_TOO_LONG";

  public const string MoneyAmountNegative = "MONEY_AMOUNT_NEGATIVE";
  public const string MoneyCurrencyRequired = "MONEY_CURRENCY_REQUIRED";
}