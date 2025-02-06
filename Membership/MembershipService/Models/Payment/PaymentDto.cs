namespace MembershipService.Models.Payment;

public class PaymentDto
{
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public int Cvc { get; set; }
    public decimal Price { get; set; }
}
