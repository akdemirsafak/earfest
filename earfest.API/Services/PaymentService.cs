using earfest.API.Models.Payment;

namespace earfest.API.Services;

public class PaymentService : IPaymentService
{
    public async Task<PayResponse> PayAsync(string cardHolderName, string cardNumber, int cvv, int expiredMonth, int expiredYear, decimal price)
    {
        //Burada Price, cardNumber, ValidThru ile payment işlemi yapılsın
        return new PayResponse
        {
            PaymentToken = Guid.NewGuid().ToString(),
            PaymentProviderCustomerId = Guid.NewGuid().ToString()
        };
    }

    public async Task<PayResponse> PayAsync(string paymentProviderCustomerId, string cardToken, decimal price)
    {
        return new PayResponse
        {
            PaymentToken = "Payment Token'dan",
            PaymentProviderCustomerId = "Payment Provider Customer ID'den"
        };
    }
}
