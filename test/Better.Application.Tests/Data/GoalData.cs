using Better.Core.Entities;

namespace Better.Application.Tests.Data;

public static class GoalData
{
    public static List<Goal> SeedGoal()
    {
        List<Goal> goals = new();

        for (int i = 1; i <= 20; i++)
        {
            int userId = i <= 10 ? 1 : 0;

            Goal goal = new()
            {
                UserId = userId,
                Title = $"Title{i}",
                Years = i * 2,
                InitialInvestment = 1_00 * 2,
                MonthlyContribution = 1_00 * 2,
                TargetAmount = 1_00 * i * 2,
                FinancialEntity = new FinancialEntity
                {
                    Title = $"FinancialEntity{i}"
                },
                Portfolio = new Portfolio
                {
                    Title = $"FinancialEntity{i}"
                },
                Created = new DateTime(2023, 01, 01),
                Modified = new DateTime(2023, 01, 01)
            };

            goals.Add(goal);
        }

        return goals;
    }
}
