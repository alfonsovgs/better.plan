using AutoMapper;
using Better.Application.Customers.DTO;
using Better.Application.Users;
using Microsoft.EntityFrameworkCore;

namespace Better.Infrastructure.Data.Queries;

public class UserQueryService : IUserQueryService
{
    private readonly ChallengeContext _dbContext;
    private readonly IMapper _mapper;

    public UserQueryService(ChallengeContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<UserDto> GetUser(int id)
    {
        var user = await _dbContext.Users
           .Include(u => u.Advisor)
           .FirstOrDefaultAsync(u => u.Id == id);

        return _mapper.Map<UserDto>(user);
    }
}
