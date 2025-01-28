using earfest.API.Domain.DbContexts;
using earfest.API.Helpers;
using earfest.API.Models.Order;
using earfest.Shared.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Features.Orders;

public static class GetOrderById
{
    public record Query(string Id) : IRequest<AppResult<OrderDetailsResponse>>;

    public class QueryHandler : IRequestHandler<Query, AppResult<OrderDetailsResponse>>
    {
        private readonly EarfestDbContext _dbContext;
        private readonly ICurrentUser _currentUser;
        public QueryHandler(EarfestDbContext dbContext, 
            ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }
        public async Task<AppResult<OrderDetailsResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .Where(x => x.Id == request.Id)
                .Select(x => new OrderDetailsResponse
                {
                    Id = x.Id,
                    PlanId = x.PlanId,
                    BuyerId = x.BuyerId,
                    PlanName = x.PlanName,
                    Price = x.Price,
                    CreatedAt = x.CreatedAt,
                    MaskedHolderName = x.HolderName,
                    MaskedCardNumber = x.CardNumber
                })
                .FirstOrDefaultAsync();

            if (order == null)
                return AppResult<OrderDetailsResponse>.Fail("Order not found");

            return AppResult<OrderDetailsResponse>.Success(order);
        }
    }

}
