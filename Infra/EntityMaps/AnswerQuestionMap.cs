using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db1HealthPanelBack.Infra.EntityMaps
{
    public class AnswerQuestionMap : IEntityTypeConfiguration<AnswerQuestion>
    {
        public void Configure(EntityTypeBuilder<AnswerQuestion> builder)
        {
            builder.HasKey(property => property.Id);
            builder.Property(property => property.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(property => property.Question)
                .WithMany();
        }
    }
}