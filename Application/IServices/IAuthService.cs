using Application.Dto;

namespace Application.IServices;

public interface IAuthService 
{
    /// <summary>
    /// Tries to log user in
    /// </summary>
    /// <param name="loginDto">Login information</param>
    /// <returns>User response with a Json Web Token</returns>
    public Task<UserResponse> LoginAsync(LoginDto loginDto);

    public Task RegisterAsync();
}
