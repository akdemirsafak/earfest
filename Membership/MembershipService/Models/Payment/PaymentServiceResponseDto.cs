namespace MembershipService.Models.Payment;

public class PaymentServiceResponseDto
{
    public string PaymentId { get; set; }
    public DateTime? PaymentDate { get; set; }
    public decimal Price { get; set; }
}
