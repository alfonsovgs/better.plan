using Better.Core.Entities;
using Better.Core.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Better.Infrastructure.Data.Repositories;

internal class TransactionMovementRepository : ITransactionMovementRepository
{
    private const string _baseQuery = @"
		select 
			g2.targetamount 			targetAmount,
			g3.amount					amount,
			g3.quotas					quotas,
			g.currencyid 				sourceCurrency,
			g2.displaycurrencyid 		displayCurrency,
			fshv.value 					fundingShareValue,
			f.isbox						isBox,
			g.""type""					type,
			last_date_quota.""date"" 	dateQuota
		from goaltransaction g
		inner join goal g2 on g2.id = g.goalid 
		inner join goaltransactionfunding g3 on g.id = g3.transactionid 
		inner join funding f on g3.fundingid = f.id
		inner join lateral (
			select g4.""date""  from goaltransactionfunding g4 
			where g4.transactionid = g.id
			order by ""date"" desc 
			limit 1
		) last_date_quota on true
		inner join lateral (
			select f2.value from fundingsharevalue f2 
			where f2.""date"" = g3.date
			limit 1
		) fshv on true
		where 1 = 1";
    
	private readonly ChallengeContext _dbContext;
    public TransactionMovementRepository(ChallengeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TransactionMovement>> GetById(int userId, int goalId)
    {
		var query = _baseQuery;
		if(userId > 0)
		{
			query += " AND g2.userid = @userId";
		}

		if(goalId > 0)
		{
			query += " AND g2.id = @goalId";
		}

		var connection = _dbContext.Database.GetDbConnection();
		var movements = await connection.QueryAsync<TransactionMovement>(query, new { userId, goalId });

        return movements;
    }
}