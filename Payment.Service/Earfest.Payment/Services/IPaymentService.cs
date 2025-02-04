using earfest.Shared.Base;
using Earfest.Payment.Models;

namespace Earfest.Payment.Services;

public interface IPaymentService
{
    Task<AppResult<PayResponse>> PayAsync(PayRequest request);
    Task<AppResult<PayResponse>> GetPaymentAsync(string paymentId);
    Task<AppResult<NoContentDto>> RefundAsync(string paymentId);
    Task<AppResult<List<PayResponse>>> GetUserPaymentsAsync(string userId);

}
