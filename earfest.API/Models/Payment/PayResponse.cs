namespace earfest.API.Models.Payment;

public class PayResponse
{
    public string PaymentToken { get; set; } // Ödeme işlemi başarılı olursa token oluşturulacak.
    public string PaymentProviderCustomerId { get; set; } // Ödeme sağlayıcısının müşteri ID'si

}
