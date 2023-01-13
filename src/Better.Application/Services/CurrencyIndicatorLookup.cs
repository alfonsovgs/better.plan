using Better.Core.Entities;
using Better.Core.Repositories;

namespace Better.Application.Services;

internal class CurrencyIndicatorLookup : ICurrencyIndicatorLookup
{
    private readonly ICurrencyIndicatorRepository _repository;

    public CurrencyIndicatorLookup(ICurrencyIndicatorRepository repository)
    {
        _repository = repository;
    }

    public async Task<CurrencyIndicator> GetBy(int sourceCurrency, int destinationCurrency, DateOnly date)
    {
        var currencyIndicator = await _repository.GetBy(sourceCurrency, destinationCurrency, date);
        return currencyIndicator ?? CurrencyIndicator.Default;
    }
}
