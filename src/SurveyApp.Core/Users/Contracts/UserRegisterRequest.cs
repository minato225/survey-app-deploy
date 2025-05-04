namespace SurveyApp.Application.Users.Contracts;

public class UserRegisterRequest
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string RepeatPassowrd { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}