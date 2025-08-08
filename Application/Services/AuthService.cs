using Application.Dto;
using Application.Exceptions;
using Application.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<AppUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager; 
        _tokenService = tokenService;
    }

    public async Task<UserResponse> LoginAsync(LoginDto loginDto)
    {
        AppUser user = await _userManager.FindByNameAsync(loginDto.UserName)
            ?? throw new UnauthorizedException("Invalid username or password.");

        if (! await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            throw new UnauthorizedException("Invalid username or password.");
        }

        string jwt = _tokenService.GenerateToken(user);

        UserResponse userResponse = new UserResponse()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = jwt
        };

        return userResponse;
    }


    public async Task RegisterAsync()
    {
        string username = "ivana";
        string email = "ivana@gmail.com";
        string password = "pass";

        AppUser user = new AppUser()
        {
            Email = email,
            UserName = username,
            FirstName = "Ivana",
            LastName = "Mudresic Miholjevic"
        };

        IdentityResult result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
            Console.WriteLine("USER ADDED SUCCESSFULLY");
        else
        {
            foreach (var error in result.Errors)
                Console.WriteLine(error.Description);
        }
    }
}
