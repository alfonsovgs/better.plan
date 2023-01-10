namespace Better.Core.Entities;

public partial class InvestmentStrategy
{
    public InvestmentStrategy()
    {
        Portfolios = new HashSet<Portfolio>();
    }

    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string[] Features { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public int FinancialEntityId { get; set; }
    public bool IsVisible { get; set; }
    public bool IsDefault { get; set; }
    public string ShortTitle { get; set; } = null!;
    public int? InvestmentStrategyTypeId { get; set; }
    public string? IconUrl { get; set; }
    public bool IsRecommended { get; set; }
    public string? RecommendedFor { get; set; }
    public int DisplayOrder { get; set; }

    public virtual FinancialEntity FinancialEntity { get; set; } = null!;
    public virtual InvestmentStrategyType? InvestmentStrategyType { get; set; }
    public virtual ICollection<Portfolio> Portfolios { get; set; }
}
