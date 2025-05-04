namespace SurveyApp.Application.Users.Contracts;

public class UserTokenResponse
{
    public Guid UserId { get; set; } = Guid.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiryTime { get; set; }
}