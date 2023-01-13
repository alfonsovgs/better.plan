namespace Better.Core.Entities;

public partial class CurrencyIndicator
{
    public static CurrencyIndicator Default = new() { Value = 1 };

    public int Id { get; set; }
    public int SourceCurrencyId { get; set; }
    public int DestinationCurrencyId { get; set; }
    public double Value { get; set; }
    public DateOnly Date { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public DateTime Requestdate { get; set; }
    public double Ask { get; set; }
    public double Bid { get; set; }

    public virtual Currency DestinationCurrency { get; set; } = null!;
    public virtual Currency SourceCurrency { get; set; } = null!;
}
