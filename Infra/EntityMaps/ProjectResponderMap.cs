using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Db1HealthPanelBack.Infra.EntityMaps;

public class ProjectResponderMap : IEntityTypeConfiguration<ProjectResponder>
{
    public void Configure(EntityTypeBuilder<ProjectResponder> builder)
    {
        builder.HasKey(property => property.Id);
        
        builder
            .Property(property => property.Id)
            .ValueGeneratedOnAdd();
        
        builder
            .Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        builder
            .Property(p => p.ProjectId)
            .IsRequired();
        
        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder
            .Property(p => p.IsLead)
            .IsRequired();
        
        builder.HasOne(x => x.Project)
            .WithMany(x => x.ProjectResponders)
            .HasForeignKey(x => x.ProjectId);
    }
}