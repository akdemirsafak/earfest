namespace earfest.API.Models.Order;

public class OrderDetailsResponse : BaseResponse
{
    public string PlanId { get; set; }
    public string PlanName { get; set; }
    public string BuyerId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string MaskedCardNumber { get; set; }
    public string MaskedHolderName { get; set; }
    public decimal Price { get; set; }
}
