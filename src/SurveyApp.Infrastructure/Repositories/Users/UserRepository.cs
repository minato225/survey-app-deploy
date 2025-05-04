using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.Repositories.Users;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task CreateAsync(AdminUser entity, CancellationToken cancellationToken)
    {
        await context.AdminUsers.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AdminUser entity, CancellationToken cancellationToken)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);
        context.Entry(entity).State = EntityState.Detached;
    }

    public async Task DeleteAsync(AdminUser entity, CancellationToken cancellationToken)
    {
        context.AdminUsers.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<AdminUser?> GetByIdOrDefaultAsync(
        Guid id,
        CancellationToken cancellationToken,
        params Expression<Func<AdminUser, object>>[]? includesProperties)
    {
        IQueryable<AdminUser>? query = context.AdminUsers.AsQueryable();
        if (includesProperties != null && includesProperties.Any())
        {
            foreach (Expression<Func<AdminUser, object>>? included in includesProperties)
            {
                query = query.Include(included);
            }
        }

        return await context.AdminUsers.FirstOrDefaultAsync(el => el.Id == id, cancellationToken);
    }

    public async Task<AdminUser?> FirstOrDefaultAsync(
        Expression<Func<AdminUser, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        return await context.AdminUsers.FirstOrDefaultAsync(filter, cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Expression<Func<AdminUser, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        return await context.AdminUsers.AnyAsync(filter, cancellationToken);
    }
}