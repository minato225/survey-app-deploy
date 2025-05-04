namespace SurveyApp.Domain.Entities;

public class VariationSet
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = null!;
}