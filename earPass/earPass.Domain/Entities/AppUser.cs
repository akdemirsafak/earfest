using Microsoft.AspNetCore.Identity;

namespace earPass.Domain.Entities;

public sealed class AppUser : IdentityUser<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Image { get; set; }
}
