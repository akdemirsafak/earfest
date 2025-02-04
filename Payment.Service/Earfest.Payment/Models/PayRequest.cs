namespace Earfest.Payment.Models;

public record PayRequest(string CardHolderName, string CardNumber, int Cvc, int ExpiryMonth, int ExpiryYear, decimal Price);

