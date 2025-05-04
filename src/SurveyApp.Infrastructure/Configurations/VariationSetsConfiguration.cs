using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.Configurations;

public class VariationSetsConfiguration : IEntityTypeConfiguration<VariationSet>
{
    public void Configure(EntityTypeBuilder<VariationSet> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(t => t.Text).IsRequired();
    }
}