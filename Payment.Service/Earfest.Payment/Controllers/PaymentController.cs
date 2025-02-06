using earfest.Shared.Base;
using Earfest.Payment.Models;
using Earfest.Payment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Earfest.Payment.Controllers;

public class PaymentController : EarfestBaseController
{
    private readonly IPaymentService _paymentService;
    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    [HttpPost("pay")]
    public async Task<IActionResult> Pay([FromBody] PayRequest request)
    {
        var response = await _paymentService.PayAsync(request);
        return CreateActionResult(response);
    }
    [HttpGet("{paymentId}")]
    public async Task<IActionResult> Get(string paymentId) // Ödeme sorgulama
    {
        var response = await _paymentService.GetPaymentAsync(paymentId);
        return CreateActionResult(response);
    }
    [HttpPost("refund/{paymentId}")]
    public async Task<IActionResult> Refund(string paymentId) // Ödeme iadesi
    {
        var response = await _paymentService.RefundAsync(paymentId);
        return CreateActionResult(response);
    }
    [HttpPost("user/{userId}")]
    public async Task<IActionResult> GetUserPayments(string userId) // Kullanıcı ödemeleri
    {
        var response = await _paymentService.GetUserPaymentsAsync(userId);
        return CreateActionResult(response);
    }

}
