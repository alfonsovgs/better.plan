namespace Better.Core.Entities;

public partial class Goal
{
    public Goal()
    {
        GoalTransactionFundings = new HashSet<GoalTransactionFunding>();
        GoalTransactions = new HashSet<GoalTransaction>();
    }

    public string Title { get; set; } = null!;
    public int Years { get; set; }
    public int InitialInvestment { get; set; }
    public int MonthlyContribution { get; set; }
    public int TargetAmount { get; set; }
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public int GoalCategoryId { get; set; }
    public int? RiskLevelId { get; set; }
    public int? PortfolioId { get; set; }
    public int? FinancialEntityId { get; set; }
    public int? CurrencyId { get; set; }
    public int? DisplayCurrencyId { get; set; }

    public virtual Currency? Currency { get; set; }
    public virtual Currency? DisplayCurrency { get; set; }
    public virtual FinancialEntity? FinancialEntity { get; set; }
    public virtual GoalCategory GoalCategory { get; set; } = null!;
    public virtual Portfolio? Portfolio { get; set; }
    public virtual Risklevel? RiskLevel { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual ICollection<GoalTransactionFunding> GoalTransactionFundings { get; set; }
    public virtual ICollection<GoalTransaction> GoalTransactions { get; set; }
}
