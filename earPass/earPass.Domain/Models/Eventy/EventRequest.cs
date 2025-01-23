namespace earPass.Domain.Models.Eventy;

public record EventRequest(
    string name, 
    DateTime? date, 
    string location, 
    int type, 
    string? description, 
    List<string>? performerIds, 
    List<string>? tickedIds, 
    string? image);