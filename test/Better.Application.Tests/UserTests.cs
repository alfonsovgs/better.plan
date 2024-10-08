﻿using AutoMapper;
using Better.Application.Common.Mappings;
using Better.Application.Queries;
using Better.Application.Tests.Data;
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
    private readonly List<Goal> _goals = GoalData.SeedGoal();

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

        var mockBalanceService = new Mock<IBalanceService>();

        IUserQueryService service = new UserQueryService(mockContext.Object, _mapper, mockBalanceService.Object);

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

        var mockBalanceService = new Mock<IBalanceService>();

        IUserQueryService service = new UserQueryService(mockContext.Object, _mapper, mockBalanceService.Object);

        var result = await service.GetUser(2);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(2);
        result.Username.ShouldBe("user2 better");
        result.Advisor.ShouldBe(string.Empty);
        result.CreatedAt.ShouldBe(new DateTime(2023, 01, 01));
    }

    [Fact]
    public async Task GetGoalsByUser_Returns_PaginatedList()
    {
        var data = _goals.AsQueryable().BuildMockDbSet();

        var mockContext = new Mock<ChallengeContext>();
        mockContext.Setup(m => m.Goals).Returns(data.Object);

        var mockBalanceService = new Mock<IBalanceService>();

        IUserQueryService service = new UserQueryService(mockContext.Object, _mapper, mockBalanceService.Object);

        var result = await service.GetGoalsByUserId(1, 1, 5);
        result.ShouldNotBeNull();
        result.Items.Count.ShouldBe(5);
        result.PageNumber.ShouldBe(1);
        result.TotalPages.ShouldBe(2);
        result.TotalCount.ShouldBe(10);
        result.HasPreviousPage.ShouldBeFalse();
        result.HasNextPage.ShouldBeTrue();
    }

    [Fact]
    public async Task GetSummaryByUserId_Returns_BalanceSummary()
    {
        var mockContext = new Mock<ChallengeContext>();
        var mockBalanceService = new Mock<IBalanceService>();

        mockBalanceService.Setup(m => m.GetBalanceByUserId(It.IsAny<int>()))
            .Returns((int id) =>
            {
                Balance balance = new()
                {
                    CurrentAmount = 1_000,
                    TargetAmount = 10_000,
                    TotalContributions = 1_000,
                    TotalWithdrawal = 0
                };
                return Task.FromResult(balance);
            });

        IUserQueryService service = new UserQueryService(mockContext.Object, _mapper,
            mockBalanceService.Object);

        var result = await service.GetSummaryByUserId(1);
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetSummaryByUserId_Returns_BalanceSummary_As_Null()
    {
         var mockContext = new Mock<ChallengeContext>();
        var mockBalanceService = new Mock<IBalanceService>();

        mockBalanceService.Setup(m => m.GetBalanceByUserId(It.IsAny<int>()))
            .Returns(Task.FromResult((Balance?)null));

        IUserQueryService service = new UserQueryService(mockContext.Object, _mapper,
            mockBalanceService.Object);

        var result = await service.GetSummaryByUserId(1);
        result.ShouldBeNull();
    }
}
