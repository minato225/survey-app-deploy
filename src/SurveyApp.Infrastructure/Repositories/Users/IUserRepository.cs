using System.Linq.Expressions;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.Repositories.Users;

public interface IUserRepository
{
    Task<AdminUser?> GetByIdOrDefaultAsync(
        Guid id,
        CancellationToken cancellationToken = default,
        params Expression<Func<AdminUser, object>>[]? includesProperties);

    Task CreateAsync(
        AdminUser entity,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        AdminUser entity,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        AdminUser entity,
        CancellationToken cancellationToken = default);

    Task<AdminUser?> FirstOrDefaultAsync(
        Expression<Func<AdminUser, bool>> filter,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        Expression<Func<AdminUser, bool>> filter,
        CancellationToken cancellationToken = default);
}