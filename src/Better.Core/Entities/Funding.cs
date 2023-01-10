namespace Better.Core.Entities;

public partial class Funding
{
    public Funding()
    {
        FundingCompositions = new HashSet<FundingComposition>();
        FundingShareValues = new HashSet<FundingShareValue>();
        GoalTransactionFundings = new HashSet<GoalTransactionFunding>();
        PortfolioFundings = new HashSet<PortfolioFunding>();
    }

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Uuid { get; set; }
    public int Id { get; set; }
    public int FinancialEntityId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string? Mnemonic { get; set; }
    public bool IsBox { get; set; }
    public string? Yahoomnemonic { get; set; }
    public string? CmfUrl { get; set; }
    public int? CurrencyId { get; set; }
    public bool HasShareValue { get; set; }

    public virtual Currency? Currency { get; set; }
    public virtual FinancialEntity FinancialEntity { get; set; } = null!;
    public virtual ICollection<FundingComposition> FundingCompositions { get; set; }
    public virtual ICollection<FundingShareValue> FundingShareValues { get; set; }
    public virtual ICollection<GoalTransactionFunding> GoalTransactionFundings { get; set; }
    public virtual ICollection<PortfolioFunding> PortfolioFundings { get; set; }
}
