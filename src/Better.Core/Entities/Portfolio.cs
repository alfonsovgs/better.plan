namespace Better.Core.Entities;

public partial class Portfolio
{
    public Portfolio()
    {
        Goals = new HashSet<Goal>();
        PortfolioCompositions = new HashSet<PortfolioComposition>();
        PortfolioFundings = new HashSet<PortfolioFunding>();
    }

    public double Maxrangeyear { get; set; }
    public double Minrangeyear { get; set; }
    public string Uuid { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int Id { get; set; }
    public int FinancialentityId { get; set; }
    public int RisklevelId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public bool IsDefault { get; set; }
    public string? Profitability { get; set; }
    public int? InvestmentstrategyId { get; set; }
    public string? Version { get; set; }
    public string? ExtraProfitabilityCurrencyCode { get; set; }
    public double EstimatedProfitability { get; set; }
    public double BpComission { get; set; }

    public virtual FinancialEntity Financialentity { get; set; } = null!;
    public virtual InvestmentStrategy? Investmentstrategy { get; set; }
    public virtual Risklevel Risklevel { get; set; } = null!;
    public virtual ICollection<Goal> Goals { get; set; }
    public virtual ICollection<PortfolioComposition> PortfolioCompositions { get; set; }
    public virtual ICollection<PortfolioFunding> PortfolioFundings { get; set; }
}
