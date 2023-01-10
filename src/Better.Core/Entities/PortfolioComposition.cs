namespace Better.Core.Entities;

public partial class PortfolioComposition
{
    public double? Percentage { get; set; }
    public int Id { get; set; }
    public int SubcategoryId { get; set; }
    public int PortfolioId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }

    public virtual Portfolio Portfolio { get; set; } = null!;
    public virtual CompositionSubcategory Subcategory { get; set; } = null!;
}
