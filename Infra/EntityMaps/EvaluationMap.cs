using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db1HealthPanelBack.Infra.EntityMaps
{
    public class EvaluationMap : IEntityTypeConfiguration<Evaluation>
    {
        public void Configure(EntityTypeBuilder<Evaluation> builder)
        {
            builder.HasKey(property => property.Id);
            builder.Property(property => property.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(property => property.Project)
                .WithMany(property => property.Evaluations);

            builder.HasOne(property => property.Answer)
                .WithOne(property => property.Evaluation)
                .HasForeignKey<Answer>(property => property.EvaluationId);
        }
    }
}