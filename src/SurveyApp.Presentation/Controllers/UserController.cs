using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Users;
using SurveyApp.Application.Users.Contracts;

namespace SurveyApp.Presentation.Controllers;

[ApiController]
[Route("api/")]
public class UserController(
    IUserService userService)
    : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
    {
        await userService.RegisterAsync(request);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        var response = await userService.LoginAsync(request);
        var accessTokenCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = response.RefreshTokenExpiryTime,
        };

        var refreshTokenCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = response.RefreshTokenExpiryTime
        };
        Response.Cookies.Append("AccessToken", response.AccessToken, accessTokenCookieOptions);
        Response.Cookies.Append("RefreshToken", response.RefreshToken, refreshTokenCookieOptions);

        return Ok(new { message = "Login successful" });
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["RefreshToken"];

        if (string.IsNullOrEmpty(refreshToken))
        {
            return Unauthorized(new { message = "Refresh token is missing." });
        }

        var accessToken = await userService.RefreshTokenAsync(refreshToken);
        var accessTokenCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddHours(6),
        };
        Response.Cookies.Append("AccessToken", accessToken, accessTokenCookieOptions);
        return Ok(new { message = "Login successful" });
    }
}