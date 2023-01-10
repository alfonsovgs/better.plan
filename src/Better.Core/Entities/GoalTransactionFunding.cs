namespace Better.Core.Entities;

public partial class GoalTransactionFunding
{
    public double Percentage { get; set; }
    public double? Amount { get; set; }
    public double? Quotas { get; set; }
    public DateOnly Date { get; set; }
    public int Id { get; set; }
    public int FundingId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public int? TransactionId { get; set; }
    public string State { get; set; } = null!;
    public string? Type { get; set; }
    public int GoalId { get; set; }
    public int OwnerId { get; set; }
    public double Cost { get; set; }

    public virtual Funding Funding { get; set; } = null!;
    public virtual Goal Goal { get; set; } = null!;
    public virtual User Owner { get; set; } = null!;
    public virtual GoalTransaction? Transaction { get; set; }
}
