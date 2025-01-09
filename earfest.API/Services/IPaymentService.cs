using earfest.API.Models.Payment;

namespace earfest.API.Services;

public interface IPaymentService
{
    Task<PayResponse> PayAsync(string cardHolderName, string cardNumber, int cvv, int expiredMonth, int expiredYear, decimal price);
    Task<PayResponse> PayAsync(string paymentProviderCustomerId, string cardToken, decimal price); 
}
