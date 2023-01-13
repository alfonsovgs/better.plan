using Better.Core.Entities;

namespace Better.Application.Services;

public interface ICurrencyIndicatorLookup
{
    Task<CurrencyIndicator> GetBy(int sourceCurrency, int destinationCurrency, DateOnly date);
}