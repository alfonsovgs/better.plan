namespace Better.Core.Entities;

public partial class User
{
    public User()
    {
        Goals = new HashSet<Goal>();
        GoalTransactionFundings = new HashSet<GoalTransactionFunding>();
        GoalTransactions = new HashSet<GoalTransaction>();
        InverseAdvisor = new HashSet<User>();
    }

    public string Firstname { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public int Id { get; set; }
    public int? AdvisorId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public int? CurrencyId { get; set; }

    public virtual User? Advisor { get; set; }
    public virtual Currency? Currency { get; set; }
    public virtual ICollection<Goal> Goals { get; set; }
    public virtual ICollection<GoalTransactionFunding> GoalTransactionFundings { get; set; }
    public virtual ICollection<GoalTransaction> GoalTransactions { get; set; }
    public virtual ICollection<User> InverseAdvisor { get; set; }
}
