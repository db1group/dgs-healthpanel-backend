using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db1HealthPanelBack.Infra.EntityMaps
{
    public class LeadMap : IEntityTypeConfiguration<Lead>
    {
        public void Configure(EntityTypeBuilder<Lead> builder)
        {
            builder.HasKey(property => new
            {
                property.ProjectId
            ,
                property.Name
            });
        }
    }
}