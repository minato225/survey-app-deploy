using SurveyApp.Application.Forms.Contracts;

namespace SurveyApp.Application.Forms;
public interface IFormService
{
    Task CreateFormAsync(CreateFormRequest request, Guid adminUserId);
}