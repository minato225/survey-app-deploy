using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.Repositories.Forms;
public interface IFormRepository
{
    Task CreateAsync(Form entity,
        CancellationToken cancellationToken = default);
}
