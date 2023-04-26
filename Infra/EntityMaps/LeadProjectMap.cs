using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db1HealthPanelBack.Infra.EntityMaps
{
    public class LeadProjectMap : IEntityTypeConfiguration<LeadProject>
    {
        public void Configure(EntityTypeBuilder<LeadProject> builder)
        {
            builder.HasKey(property => new
            {
                property.LeadId,
                property.ProjectId
            });

            builder.HasOne(property => property.Lead)
                .WithMany(property => property.LeadProjects);

            builder.HasOne(property => property.Project)
                .WithMany(property => property.LeadProjects);

        }
    }
}