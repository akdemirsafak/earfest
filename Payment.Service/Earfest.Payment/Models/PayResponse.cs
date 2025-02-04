namespace Earfest.Payment.Models;
public class PayResponse
{
    //public string Id { get; set; }
    //public string PaymentToken { get; set; } // Ödeme işlemi başarılı olursa token oluşturulacak.
    //public string PaymentProviderCustomerId { get; set; } // Ödeme sağlayıcısının müşteri ID'si
    public string PaymentId { get; set; }
    public DateTime? PaymentDate { get; set; }
    public decimal Price { get; set; }

}
