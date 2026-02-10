using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantControlPanel.Domain;

namespace PlantControlPanel.Infrastructure.Persistence.Configuration;

public class RollDbConfiguration : IEntityTypeConfiguration<Roll>
{
    public void Configure(EntityTypeBuilder<Roll> builder)
    {
        builder.ToTable("Rolls");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Weight).IsRequired();
        builder.Property(x => x.Length).IsRequired();
        
        builder.Property(x => x.AddTime).HasColumnType("timestamp with time zone");
        builder.Property(x => x.RemoveTime).HasColumnType("timestamp with time zone");
    }
}