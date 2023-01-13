namespace Better.Core.Entities;

public class TransactionMovement
{
    public decimal TargetAmount { get; set; }
    public decimal Amount { get; set; }
    public decimal Quotas { get; set; }
    public int SourceCurrency { get; set; }
    public int DisplayCurrency { get; set; }
    public decimal FundingShareValue { get; set; }
    public bool IsBox { get; set; }
    public string Type { get; set; }
    public DateTime DateQuota { get; set; }
}

