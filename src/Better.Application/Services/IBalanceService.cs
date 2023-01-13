using Better.Core.Entities;

namespace Better.Application;

public interface IBalanceService
{
    Task<Balance> GetBalanceByUserId(int userId);
}