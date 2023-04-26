using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db1HealthPanelBack.Infra.EntityMaps
{
    public class AnswerPillarMap : IEntityTypeConfiguration<AnswerPillar>
    {
        public void Configure(EntityTypeBuilder<AnswerPillar> builder)
        {
            builder.HasKey(property => property.Id);
            builder.Property(property => property.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(property => property.Pillar)
                .WithMany();
        }
    }
}