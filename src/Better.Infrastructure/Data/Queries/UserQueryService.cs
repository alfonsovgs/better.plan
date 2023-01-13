using AutoMapper;
using AutoMapper.QueryableExtensions;
using Better.Application;
using Better.Application.Common.Models;
using Better.Application.DTO;
using Better.Application.Queries;
using Better.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Better.Infrastructure.Data.Queries;

public class UserQueryService : IUserQueryService
{
    private readonly ChallengeContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IBalanceService _balanceService;

    public UserQueryService(ChallengeContext dbContext, IMapper mapper, IBalanceService balanceService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _balanceService = balanceService;
    }

    public async Task<UserDto> GetUser(int id)
    {
        var user = await _dbContext.Users
           .Include(u => u.Advisor)
           .FirstOrDefaultAsync(u => u.Id == id);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<PaginatedList<GoalDto>> GetGoalsByUserId(int id, int pageNumber, int pageSize)
    {
        return await _dbContext.Goals
            .Include(g => g.FinancialEntity)
            .Include(g => g.Portfolio)
            .Where(g => g.UserId == id)
            .OrderByDescending(g => g.Created)
            .ProjectTo<GoalDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(pageNumber, pageSize);
    }

    public async Task<SummaryDto> GetSummaryByUserId(int id)
    {
        var result = await _balanceService.GetBalanceByUserId(id);
        return _mapper.Map<SummaryDto>(result);
    }
}
