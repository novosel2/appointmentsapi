using Domain.Entities;

namespace Application.IServices;

public interface ITokenService
{
    /// <summary>
    /// Generate a Json Web Token for a specified user
    /// </summary>
    /// <param name="appUser">App user object</param>
    /// <returns>Json Web Token</returns>
    public string GenerateToken(AppUser appUser);
}
