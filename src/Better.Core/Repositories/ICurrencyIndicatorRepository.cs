using Better.Core.Entities;

namespace Better.Core.Repositories;

public interface ICurrencyIndicatorRepository
{
    Task<CurrencyIndicator> GetBy(int sourceCurrency, int destinationCurrency, DateOnly date);
}