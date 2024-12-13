namespace TaskManager.Domain.ValuObjects;

public class Entity
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}
