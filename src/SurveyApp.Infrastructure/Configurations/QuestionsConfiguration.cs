using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.Configurations;

public class QuestionsConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(t => t.Title).IsRequired();
        builder.Property(t => t.Type).IsRequired();

        builder
           .HasMany(q => q.VariationSets)
           .WithOne(v => v.Question)
           .HasForeignKey(v => v.QuestionId)
           .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(q => q.UserAnswers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}