namespace Better.Core.Entities;

public class Balance
{
    public decimal CurrentAmount { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal Percentaje => CurrentAmount * 100 / TargetAmount;
    public decimal TotalWithdrawal { get; set; }
    public decimal TotalContributions { get; set; }
    public decimal Total => TotalContributions + TotalWithdrawal; 
}