namespace Better.Core.Entities;

public partial class CompositionCategory
{
    public CompositionCategory()
    {
        CompositionSubcategories = new HashSet<CompositionSubcategory>();
    }

    public string Name { get; set; } = null!;
    public string? Uuid { get; set; }
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }

    public virtual ICollection<CompositionSubcategory> CompositionSubcategories { get; set; }
}
