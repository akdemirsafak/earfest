using Mapster;
using MembershipService.Entities;
using MembershipService.Models.Subscriptions;

namespace MembershipService.Mapping
{
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Subscription, SubscriptionResponse>.NewConfig()
                .Map(dest => dest.StatuId, src => (int)src.Statu); // Enum veya ID değerini mapler
        }
    }
}
