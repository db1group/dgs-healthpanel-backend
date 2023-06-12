using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db1HealthPanelBack.Infra.EntityMaps
{
    public class ColumnMap : IEntityTypeConfiguration<Column>
    {
        public void Configure(EntityTypeBuilder<Column> builder)
        {
            builder.HasKey(property => property.Id);
            builder.Property(property => property.Id)
                .ValueGeneratedOnAdd();

            builder.HasMany(property => property.Questions)
                .WithOne()
                .HasForeignKey(property => property.ColumnId);
        }
    }
}