using Better.Core.Entities;

namespace Better.Application.Tests.Data;

public static class UserData
{
    public static List<User> SeedUsers()
    {
        List<User> users = new();

        for (int i = 1; i <= 10; i++)
        {
            var advisor = new User
            {
                Id = i,
                Firstname = $"advisor{i}",
                Surname = "better",
                Created = new DateTime(2023, 01, 01),
                Modified = new DateTime(2023, 01, 01),
            };

            users.Add(new User
            {
                Id = i,
                Firstname = $"user{i}",
                Surname = "better",
                Created = new DateTime(2023, 01, 01),
                Modified = new DateTime(2023, 01, 01),
                AdvisorId = advisor.Id % 2 == 0 ? null : advisor.Id,
                Advisor = advisor.Id % 2 == 0 ? null : advisor
            });
        }

        return users;
    }
}
