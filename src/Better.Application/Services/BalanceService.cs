using Better.Core.Entities;
using Better.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Better.Application.Services;

internal class BalanceService : IBalanceService
{
    private readonly ITransactionMovementRepository _repository;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private const int _defaultQuota = 1;

    public BalanceService(ITransactionMovementRepository repository, IServiceScopeFactory serviceScopeFactory)
    {
        _repository = repository;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<Balance> GetBalanceByUserId(int userId)
    {
        var movements = await _repository.GetById(userId, 0);
        if(!movements.Any())
        {
            return null;
        }

        return await CalculateBalance(movements);
    }

    public async Task<Balance> GetBalanceByGoalId(int userId, int goalId)
    {
        var movements = await _repository.GetById(userId, goalId);
        if(!movements.Any())
        {
            return null;
        }

        return await CalculateBalance(movements);
    }

    private async Task<Balance> CalculateBalance(IEnumerable<TransactionMovement> transactionMovements)
    {
        Balance balance = new()
        {
            TargetAmount = transactionMovements.FirstOrDefault().TargetAmount,
            TotalWithdrawal = transactionMovements.Where(m => m.Type == "sale").Sum(x => x.Amount),
            TotalContributions = transactionMovements.Where(m => m.Type == "buy").Sum(x => x.Amount),
        };

        var movementsGrouped = transactionMovements.GroupBy(m => new
        {
            m.SourceCurrency,
            m.DisplayCurrency,
            m.DateQuota
        });

        ParallelOptions parallelOptions = new()
        {
            MaxDegreeOfParallelism = 3
        };

        await Parallel.ForEachAsync(movementsGrouped, parallelOptions, async (group, token) =>
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var currencyIndicatorLookup = scope.ServiceProvider.GetService<ICurrencyIndicatorLookup>();

            var movementRoot = group.FirstOrDefault();
            var currencyIndicator = await currencyIndicatorLookup.GetBy(movementRoot.SourceCurrency,
                movementRoot.DisplayCurrency, DateOnly.FromDateTime(movementRoot.DateQuota));

            foreach (var movement in group)
            {
                if (movement.IsBox)
                {
                    movement.Quotas = _defaultQuota;
                }

                lock(balance)
                balance.CurrentAmount += movement.Quotas * movement.FundingShareValue * Convert.ToDecimal(currencyIndicator.Value);
            }
        });

        return balance;
    }
}