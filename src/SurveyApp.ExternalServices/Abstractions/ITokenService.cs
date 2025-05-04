using SurveyApp.Domain.Entities;

namespace SurveyApp.ExternalServices.Abstractions;

public interface ITokenService
{
    string GenerateAccessToken(AdminUser user);
    string GenerateRefreshToken();
}