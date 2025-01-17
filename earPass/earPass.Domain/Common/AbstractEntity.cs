namespace earPass.Domain.Common;

public abstract class AbstractEntity
{
    protected AbstractEntity()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }
}
