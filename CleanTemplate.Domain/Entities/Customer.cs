namespace CleanTemplate.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Address { get; set; }
    public string? city { get; set; }
    
}
