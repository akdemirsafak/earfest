namespace earfest.API.Domain.Entities;

public abstract class AbstractEntity
{
    public string Id { get; set; }
    protected AbstractEntity()
    {
        Id = System.Guid.NewGuid().ToString();
    }
}
