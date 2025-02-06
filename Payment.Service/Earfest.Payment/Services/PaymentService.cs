using earfest.Shared.Base;
using Earfest.Payment.DbContexts;
using Earfest.Payment.Entities;
using Earfest.Payment.Models;

namespace Earfest.Payment.Services;

public class PaymentService : IPaymentService
{
    private readonly PaymentDbContext _dbContext;

    public PaymentService(PaymentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResult<PayResponse>> GetPaymentAsync(string paymentId)
    {
        PayResponse payment =
            new PayResponse
            {
                PaymentId = paymentId,
                PaymentDate = DateTime.UtcNow,
                Price = 100
            };

        return AppResult<PayResponse>.Success(payment);

    }

    public Task<AppResult<List<PayResponse>>> GetUserPaymentsAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<AppResult<PayResponse>> PayAsync(PayRequest request)
    {
        //pay here

        //add database
        var payment = new PaymentEntity
        {
            BuyerId = Guid.NewGuid().ToString(),
            CardNumber = request.CardNumber,
            HolderName = request.CardHolderName,
            Price = request.Price,
            CreatedAt = DateTime.UtcNow,
            Statu = PaymentStatu.Success
        };

        await _dbContext.Payments.AddAsync(payment);
        await _dbContext.SaveChangesAsync();

        var response = new PayResponse
        {

            PaymentId = payment.Id,
            PaymentDate = payment.CreatedAt,
            Price = payment.Price
        };

        return AppResult<PayResponse>.Success(response, 201);
    }



    public Task<AppResult<NoContentDto>> RefundAsync(string paymentId)
    {
        throw new NotImplementedException();
    }
}
