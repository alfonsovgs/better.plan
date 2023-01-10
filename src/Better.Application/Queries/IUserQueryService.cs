using Better.Application.Common.Models;
using Better.Application.DTO;

namespace Better.Application.Queries;

public interface IUserQueryService
{
    Task<UserDto> GetUser(int id);
    Task<PaginatedList<GoalDto>> GetGoalsByUserId(int id, int pageNumber, int pageSize);
}
