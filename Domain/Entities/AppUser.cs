using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

#pragma warning disable
public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public override string Email { get; set; } = string.Empty;
    public override string UserName { get; set; } = string.Empty;

    public AppUser()
    {
        Id = Guid.NewGuid();
    }
}
