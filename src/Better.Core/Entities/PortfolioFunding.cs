namespace Better.Core.Entities;

public partial class PortfolioFunding
{
    public double Percentage { get; set; }
    public int Id { get; set; }
    public int FundingId { get; set; }
    public int PortfolioId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }

    public virtual Funding Funding { get; set; } = null!;
    public virtual Portfolio Portfolio { get; set; } = null!;
}
