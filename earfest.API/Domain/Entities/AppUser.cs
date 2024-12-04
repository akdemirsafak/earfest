using Microsoft.AspNetCore.Identity;

namespace earfest.API.Domain.Entities;

public class AppUser : IdentityUser<string>
{
    public AppUser()
    {
        Contents = new HashSet<Content>();
        Playlists = new HashSet<Playlist>();
        Plan = new Plan();
    }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? ImageUrl { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
    public ICollection<Content>? Contents { get; set; }
    public ICollection<Playlist>? Playlists { get; set; }
    public Plan? Plan { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
