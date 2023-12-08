using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db1HealthPanelBack.Infra.EntityMaps
{
    class KeyDB1CLIMap : IEntityTypeConfiguration<KeyDB1CLI>
    {
        public void Configure(EntityTypeBuilder<KeyDB1CLI> builder)
        {
            builder.HasKey(property => property.Key);
            builder.Property(property => property.Key)
                .ValueGeneratedOnAdd();
        }
    }
}