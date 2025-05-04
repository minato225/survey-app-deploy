using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using SurveyApp.Application.Users.Contracts;
using SurveyApp.Domain.Entities;
using SurveyApp.ExternalServices.Abstractions;
using SurveyApp.Infrastructure.Repositories.Users;

namespace SurveyApp.Application.Users;

public class UserService : IUserService
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserService(ITokenService tokenService, IUserRepository userRepository, IPasswordHasher passwordHasher, IHttpContextAccessor httpContextAccessor)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<UserTokenResponse> LoginAsync(UserLoginRequest request)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var user = await _userRepository.FirstOrDefaultAsync(u => u.Login == request.Login);
        if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new ArgumentException("Invalid email or password");
        }

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var sha256 = SHA256.Create();
        var refreshTokenHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshToken));
        user.RefreshToken = Convert.ToBase64String(refreshTokenHash);
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userRepository.UpdateAsync(user);

        var tokenResponse = new UserTokenResponse
        {
            UserId = user.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTime.SpecifyKind((DateTime)user.RefreshTokenExpiryTime, DateTimeKind.Utc),
        };
        return tokenResponse;
    }
    public async Task RegisterAsync(UserRegisterRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var loginExist = await _userRepository.ExistsAsync(u => u.Login == request.Login);
        if (loginExist == true)
            throw new DuplicateNameException("User with the given username already exists");

        var emailExist = await _userRepository.ExistsAsync(u => u.Email == request.Email);
        if (emailExist == true)
            throw new DuplicateNameException("User with the given email already exists");

        if (request.Password != request.RepeatPassowrd)
            throw new Exception("Passwords do not match");

        var user = new AdminUser
        {
            Login = request.Login,
            Email = request.Email,
            PasswordHash = _passwordHasher.Generate(request.Password)
        };
        await _userRepository.CreateAsync(user);
    }
    public async Task DeleteAsync(Guid id)
    {
        var user = await _userRepository.GetByIdOrDefaultAsync(id);
        if (user == null)
            throw new Exception("User not found");
        await _userRepository.DeleteAsync(user);
    }
    public Guid? GetUserId()
    {
        var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (Guid.TryParse(userIdString, out var userId))
        {
            return userId;
        }
        else
        {
            return null;
        }
    }
    public async Task<string> RefreshTokenAsync(string refreshToken)
    {

        using var sha256 = SHA256.Create();
        var refreshTokenHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshToken));
        var hashedRefreshToken = Convert.ToBase64String(refreshTokenHash);

        var user = await _userRepository.FirstOrDefaultAsync(u => u.RefreshToken == hashedRefreshToken);
        if (user == null)
        {
            throw new Exception("Invalid refresh token");
        }
        if (user.RefreshTokenExpiryTime < DateTime.Now)
        {

            throw new Exception("Refresh token expired");
        }
        var newAccessToken = _tokenService.GenerateAccessToken(user);
        return newAccessToken;
    }
}