using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db1HealthPanelBack.Infra.EntityMaps
{
    public class SonarMetricMap : IEntityTypeConfiguration<SonarMetric>
    {
        public void Configure(EntityTypeBuilder<SonarMetric> builder)
        {
            builder.HasKey(prop => prop.Key);
        }
    }
}