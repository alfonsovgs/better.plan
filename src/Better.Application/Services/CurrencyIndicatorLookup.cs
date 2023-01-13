using Better.Core.Entities;
using Better.Core.Repositories;
using System.Collections.Concurrent;

namespace Better.Application.Services;

internal class CurrencyIndicatorLookup : ICurrencyIndicatorLookup
{
    private readonly ConcurrentDictionary<string, CurrencyIndicator> _currencyIndicatorLookup = new();
    private readonly ICurrencyIndicatorRepository _repository;

    public CurrencyIndicatorLookup(ICurrencyIndicatorRepository repository)
    {
        _repository = repository;
    }

    public async Task<CurrencyIndicator> GetBy(int sourceCurrency, int destinationCurrency, DateOnly date)
    {
        var currencyIndicator = await _repository.GetBy(sourceCurrency, destinationCurrency, date);
        return currencyIndicator ?? CurrencyIndicator.Default;
        /*string key = $"src{sourceCurrency}:dst{destinationCurrency}:date{date}";

        if (!_currencyIndicatorLookup.TryGetValue(key, out var value))
        {
            var currencyIndicator = await _repository.GetBy(sourceCurrency, destinationCurrency, date);

            value = currencyIndicator ?? CurrencyIndicator.Default;
            _currencyIndicatorLookup.TryAdd(key, value);
        }

        return value;*/
    }
}
