namespace Better.Core.Entities;

public partial class Currency
{
    public Currency()
    {
        CurrencyIndicatorDestinationCurrencies = new HashSet<CurrencyIndicator>();
        CurrencyIndicatorSourceCurrencies = new HashSet<CurrencyIndicator>();
        FinancialEntities = new HashSet<FinancialEntity>();
        Fundings = new HashSet<Funding>();
        GoalCurrencies = new HashSet<Goal>();
        GoalDisplaycurrencies = new HashSet<Goal>();
        GoalTransactions = new HashSet<GoalTransaction>();
        Users = new HashSet<User>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string Uuid { get; set; } = null!;
    public string? Yahoomnemonic { get; set; }
    public string? CurrencyCode { get; set; }
    public string? DigitsInfo { get; set; }
    public string? Display { get; set; }
    public string? Locale { get; set; }
    public string? ServerFormatNumber { get; set; }

    public virtual ICollection<CurrencyIndicator> CurrencyIndicatorDestinationCurrencies { get; set; }
    public virtual ICollection<CurrencyIndicator> CurrencyIndicatorSourceCurrencies { get; set; }
    public virtual ICollection<FinancialEntity> FinancialEntities { get; set; }
    public virtual ICollection<Funding> Fundings { get; set; }
    public virtual ICollection<Goal> GoalCurrencies { get; set; }
    public virtual ICollection<Goal> GoalDisplaycurrencies { get; set; }
    public virtual ICollection<GoalTransaction> GoalTransactions { get; set; }
    public virtual ICollection<User> Users { get; set; }
}
