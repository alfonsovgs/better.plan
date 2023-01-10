namespace Better.Core.Entities;

public partial class InvestmentStrategyType
{
    public InvestmentStrategyType()
    {
        InvestmentStrategies = new HashSet<InvestmentStrategy>();
    }

    public int Id { get; set; }
    public string IconUrl { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string ShortTitle { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string RecommendedFor { get; set; } = null!;
    public bool IsVisible { get; set; }
    public bool IsDefault { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public int? FinancialentityId { get; set; }

    public virtual FinancialEntity? FinancialEntity { get; set; }
    public virtual ICollection<InvestmentStrategy> InvestmentStrategies { get; set; }
}
