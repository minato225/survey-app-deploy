using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.Repositories.Forms;
public class FormRepository(AppDbContext context) : IFormRepository
{
    public async Task CreateAsync(Form entity, CancellationToken cancellationToken = default)
    {
        await context.Forms.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
