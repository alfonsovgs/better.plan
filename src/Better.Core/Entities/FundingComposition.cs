namespace Better.Core.Entities;

public partial class FundingComposition
{
    public double Percentage { get; set; }
    public int Id { get; set; }
    public int SubcategoryId { get; set; }
    public int FundingId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }

    public virtual Funding Funding { get; set; } = null!;
    public virtual CompositionSubcategory SubCategory { get; set; } = null!;
}
