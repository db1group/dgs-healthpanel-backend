using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db1HealthPanelBack.Infra.EntityMaps
{
    public class PillarMap : IEntityTypeConfiguration<Pillar>
    {
        public void Configure(EntityTypeBuilder<Pillar> builder)
        {
            builder.HasKey(property => property.Id);
            builder.Property(property => property.Id)
                .ValueGeneratedOnAdd();

            builder.HasMany(property => property.Columns)
                .WithOne()
                .HasForeignKey(property => property.PillarId);
        }
    }
}