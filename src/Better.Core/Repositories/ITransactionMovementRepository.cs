using Better.Core.Entities;

namespace Better.Core.Repositories;

public interface ITransactionMovementRepository
{
    Task<IEnumerable<TransactionMovement>> GetById(int userId, int goalId);
}