using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Helpers;
using earfest.API.Models.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Features.Orders;

public static class GetOrders
{
    public record Query : IRequest<AppResult<List<OrderResponse>>>;
    public class QueryHandler : IRequestHandler<Query, AppResult<List<OrderResponse>>>
    {
        private readonly EarfestDbContext _dbContext;
        private readonly ICurrentUser _currentUser;

        public QueryHandler(EarfestDbContext dbContext, 
            ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task<AppResult<List<OrderResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var orders= await _dbContext.Orders
                .Where(x => x.BuyerId == _currentUser.GetUserId)
                .Select(x => new OrderResponse
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
                .ToListAsync();

            return AppResult<List<OrderResponse>>.Success(orders);
        }
    }
}
