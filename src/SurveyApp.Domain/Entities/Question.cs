using SurveyApp.Domain.Enums;

namespace SurveyApp.Domain.Entities;

public class Question
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public QuestionType Type { get; set; }
    public bool IsRequired { get; set; }
    public ICollection<VariationSet>? VariationSets { get; set; } 
    public ICollection<UserAnswer> UserAnswers { get; set; } = [];
    public Guid FormId { get; set; }
    public Form Form { get; set; } = null!;
}