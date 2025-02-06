namespace MembershipService.Models.Subscriptions;

public record SubscribeRequest(string PlanId, string CardHolderName, string CardNumber, int Cvc, int ExpiryMonth, int ExpiryYear);