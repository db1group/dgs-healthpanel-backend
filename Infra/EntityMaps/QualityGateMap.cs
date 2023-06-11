using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db1HealthPanelBack.Infra.EntityMaps
{
    public class QualityGateMap : IEntityTypeConfiguration<QualityGate>
    {
        public void Configure(EntityTypeBuilder<QualityGate> builder)
        {
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Id)
                .ValueGeneratedOnAdd();
        }
    }
}