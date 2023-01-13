using Better.Core.Entities;
using Better.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Better.Infrastructure.Data.Repositories;

internal class CurrencyIndicatorRepository : ICurrencyIndicatorRepository
{
    private readonly ChallengeContext _dbContext;

    public CurrencyIndicatorRepository(ChallengeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CurrencyIndicator> GetBy(int sourceCurrency, int destinationCurrency, DateOnly date)
    {
        var currencyIndicator = await _dbContext.CurrencyIndicators
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.SourceCurrencyId == sourceCurrency
                && c.DestinationCurrencyId == destinationCurrency && c.Date == date);

        return currencyIndicator;
    }
}
