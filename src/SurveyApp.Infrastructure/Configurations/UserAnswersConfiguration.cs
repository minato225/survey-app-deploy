using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.Configurations;

public class UserAnswersConfiguration : IEntityTypeConfiguration<UserAnswer>
{
    public void Configure(EntityTypeBuilder<UserAnswer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(t => t.QuestionId).IsRequired();

        builder
            .HasOne(a => a.VariationSet)
            .WithMany()
            .HasForeignKey(a => a.VariationSetId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}