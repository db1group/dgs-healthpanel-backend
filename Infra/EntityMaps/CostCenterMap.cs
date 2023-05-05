using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db1HealthPanelBack.Infra.EntityMaps
{
    public class CostCenterMap : IEntityTypeConfiguration<CostCenter>
    {
        public void Configure(EntityTypeBuilder<CostCenter> builder)
        {
            builder.HasKey(property => property.Id);

            builder.HasMany(x => x.Projects)
                .WithOne()
                .HasForeignKey(property => property.CostCenterId);
        }
    }
}