using SurveyApp.Domain.Enums;

namespace SurveyApp.Application.Forms.Contracts;
public class CreateFormRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<CreateQuestionRequest> Questions { get; set; } = [];
}
public class CreateQuestionRequest
{
    public required string Title { get; set; }
    public QuestionType Type { get; set; }
    public bool IsRequired { get; set; }
    public List<string> Variations { get; set; } = [];
}