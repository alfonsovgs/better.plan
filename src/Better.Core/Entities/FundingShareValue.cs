namespace Better.Core.Entities;

public partial class FundingShareValue
{
    public double Value { get; set; }
    public DateOnly Date { get; set; }
    public int Id { get; set; }
    public int FundingId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }

    public virtual Funding Funding { get; set; } = null!;
}
