using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db1HealthPanelBack.Infra.EntityMaps
{
    public class StackMap : IEntityTypeConfiguration<Stack>
    {
        public void Configure(EntityTypeBuilder<Stack> builder)
        {
            builder.HasKey(property => property.Id);
            
            builder
                .Property(property => property.Id)
                .HasMaxLength(20);

            builder
                .Property(property => property.Name)
                .HasMaxLength(50);

        }
    }
}
