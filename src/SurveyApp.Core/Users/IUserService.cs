using SurveyApp.Application.Users.Contracts;

namespace SurveyApp.Application.Users;

public interface IUserService
{
    Task RegisterAsync(UserRegisterRequest request);
    Task<UserTokenResponse> LoginAsync(UserLoginRequest request);
    Guid? GetUserId();
    //Task<UserResponse> GetByIdAsync(Guid id);
    //Task<UserResponse> UpdateAsync(Guid id, UpdateUserRequest request);
    Task DeleteAsync(Guid id);
    //Task<RevokeRefreshTokenResponse> RevokeRefreshToken(RefreshTokenRequest refreshTokenRemoveRequest);
    Task<string> RefreshTokenAsync(string refreshToken);
}