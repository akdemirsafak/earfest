namespace earPass.Domain.Models.Eventy;

public class EventResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string? Image { get; set; }
    public string Type { get; set; }
    public int TypeId { get; set; }

}
