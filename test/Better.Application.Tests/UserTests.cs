using AutoMapper;
using Better.Application.Common.Mappings;
using Better.Application.Tests.Data;
using Better.Application.Users;
using Better.Core.Entities;
using Better.Infrastructure.Data;
using Better.Infrastructure.Data.Queries;
using MockQueryable.Moq;
using Moq;
using Shouldly;
using Xunit;

namespace Better.Application.Tests;

public class UserTests
{
    private readonly IMapper _mapper;
    private readonly List<User> _users = UserData.SeedUsers();

    public UserTests()
    {
        MapperConfiguration mapperConfig = new(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetUser_ReturnsUserDto()
    {
        var data = _users.AsQueryable().BuildMockDbSet();

        var mockContext = new Mock<ChallengeContext>();
        mockContext.Setup(m => m.Users).Returns(data.Object);

        IUserQueryService service = new UserQueryService(mockContext.Object, _mapper);

        var result = await service.GetUser(1);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
        result.Username.ShouldBe("user1 better");
        result.Advisor.ShouldBe("advisor1 better");
        result.CreatedAt.ShouldBe(new DateTime(2023, 01, 01));
    }

    [Fact]
    public async Task GetUser_ReturnsUserDto_With_EmptyAdvisor()
    {
        var data = _users.AsQueryable().BuildMockDbSet();

        var mockContext = new Mock<ChallengeContext>();
        mockContext.Setup(m => m.Users).Returns(data.Object);

        IUserQueryService service = new UserQueryService(mockContext.Object, _mapper);

        var result = await service.GetUser(2);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(2);
        result.Username.ShouldBe("user2 better");
        result.Advisor.ShouldBe(string.Empty);
        result.CreatedAt.ShouldBe(new DateTime(2023, 01, 01));
    }
}
