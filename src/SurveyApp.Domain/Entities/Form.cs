namespace SurveyApp.Domain.Entities;

public class Form
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Question>? Questions { get; set; }
    public string? Link { get; set; }
    public Guid AdminUserId { get; set; }
    public AdminUser AdminUser { get; set; } = null!;
}