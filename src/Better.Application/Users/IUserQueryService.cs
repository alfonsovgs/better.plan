using Better.Application.Customers.DTO;

namespace Better.Application.Users;

public interface IUserQueryService
{
    Task<UserDto> GetUser(int id);
}
