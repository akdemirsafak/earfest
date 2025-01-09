namespace earfest.API.Models.Order;

public class OrderResponse : BaseResponse
{
    public string PlanId { get; set; }
    public string PlanName { get; set; }
    public string BuyerId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public decimal Price { get; set; }
    public string MaskedCardNumber { get; set; }
    public string MaskedHolderName { get; set; }
    public string Token { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; } // Sipariş iptal edildiyse.
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
