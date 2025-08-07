using Application.Dto;
using Application.Exceptions;
using Application.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName)
            ?? throw new UnauthorizedException("Invalid username or password.");

        Console.WriteLine($"PASSWORD HASH: {user.PasswordHash}{user.UserName}{user.Email}");

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
            LastName = user.LastName
        };

        return userResponse;
    }


    public async Task RegisterAsync()
    {
        string username = "ivana";
        string email = "ivana@gmail.com";
        string password = "password123";

        AppUser user = new AppUser()
        {
            Email = email,
            UserName = username,
            FirstName = "Ivana",
            LastName = "Mudresic Miholjevic"
        };

        await _userManager.DeleteAsync(user);
        await _userManager.CreateAsync(user, password);
    }
}
