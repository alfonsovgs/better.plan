namespace Better.Core.Entities;

public partial class FinancialEntity
{
    public FinancialEntity()
    {
        Fundings = new HashSet<Funding>();
        Goals = new HashSet<Goal>();
        GoalTransactions = new HashSet<GoalTransaction>();
        InvestmentStrategies = new HashSet<InvestmentStrategy>();
        InvestmentStrategyTypes = new HashSet<InvestmentStrategyType>();
        Portfolios = new HashSet<Portfolio>();
    }

    public string? Logo { get; set; }
    public string Title { get; set; } = null!;
    public string? Uuid { get; set; }
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string? Description { get; set; }
    public int? DefaultcurrencyId { get; set; }

    public virtual Currency? DefaultCurrency { get; set; }
    public virtual ICollection<Funding> Fundings { get; set; }
    public virtual ICollection<Goal> Goals { get; set; }
    public virtual ICollection<GoalTransaction> GoalTransactions { get; set; }
    public virtual ICollection<InvestmentStrategy> InvestmentStrategies { get; set; }
    public virtual ICollection<InvestmentStrategyType> InvestmentStrategyTypes { get; set; }
    public virtual ICollection<Portfolio> Portfolios { get; set; }
}
