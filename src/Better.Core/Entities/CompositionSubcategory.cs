namespace Better.Core.Entities;

public partial class CompositionSubcategory
{
    public CompositionSubcategory()
    {
        FundingCompositions = new HashSet<FundingComposition>();
        PortfolioCompositions = new HashSet<PortfolioComposition>();
    }

    public string Name { get; set; } = null!;
    public string? Uuid { get; set; }
    public string? Description { get; set; }
    public int Id { get; set; }
    public int? CategoryId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }

    public virtual CompositionCategory? Category { get; set; }
    public virtual ICollection<FundingComposition> FundingCompositions { get; set; }
    public virtual ICollection<PortfolioComposition> PortfolioCompositions { get; set; }
}
