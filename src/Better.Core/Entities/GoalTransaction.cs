namespace Better.Core.Entities;

public partial class GoalTransaction
{
    public GoalTransaction()
    {
        GoalTransactionFundings = new HashSet<GoalTransactionFunding>();
    }

    public string Type { get; set; } = null!;
    public double? Amount { get; set; }
    public DateOnly Date { get; set; }
    public int Id { get; set; }
    public int? GoalId { get; set; }
    public int OwnerId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public bool IsProcessed { get; set; }
    public bool All { get; set; }
    public string State { get; set; } = null!;
    public int? FinancialEntityId { get; set; }
    public int CurrencyId { get; set; }
    public double Cost { get; set; }

    public virtual Currency Currency { get; set; } = null!;
    public virtual FinancialEntity? FinancialEntity { get; set; }
    public virtual Goal? Goal { get; set; }
    public virtual User Owner { get; set; } = null!;
    public virtual ICollection<GoalTransactionFunding> GoalTransactionFundings { get; set; }
}
