using SurveyApp.Application.Forms.Contracts;
using SurveyApp.Domain.Entities;
using SurveyApp.Infrastructure.Repositories.Forms;

namespace SurveyApp.Application.Forms;
public class FormService : IFormService
{
    private readonly IFormRepository _formRepository;

    public FormService(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }
    public async Task CreateFormAsync(CreateFormRequest request, Guid adminUserId)
    {
        var form = new Form
        {
            Name = request.Name,
            Description = request.Description,
            AdminUserId = adminUserId,
            Questions = [.. request.Questions.Select(q => new Question
            {
                Id = Guid.NewGuid(),
                Title = q.Title,
                Type = q.Type,
                IsRequired = q.IsRequired,
                VariationSets = [.. q.Variations.Select(v => new VariationSet
                {
                    Text = v
                })]
            })]
        };
        await _formRepository.CreateAsync(form);
    }
}
