using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db1HealthPanelBack.Infra.EntityMaps
{
    public class StackProjectMap : IEntityTypeConfiguration<StackProject>
    {
        public void Configure(EntityTypeBuilder<StackProject> builder)
        {
            builder.HasKey(property => new
            {
                property.StackId,
                property.ProjectId
            });

            builder
                .Property(property => property.StackId)
                .HasMaxLength(20);

            builder.Property(property => property.Active)
                .HasDefaultValue(true);

            builder.HasOne(x => x.Project)
                .WithMany(x => x.StackProjects)
                .HasForeignKey(x => x.ProjectId);

            builder.HasOne(x => x.Stack)
                .WithMany(x => x.StackProjects)
                .HasForeignKey(x => x.StackId);
        }
    }
}
