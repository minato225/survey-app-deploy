namespace SurveyApp.Domain.Entities;

public class UserAnswer
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = null!;
    public Guid? VariationSetId { get; set; }
    public VariationSet VariationSet { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime? Date { get; set; }
}