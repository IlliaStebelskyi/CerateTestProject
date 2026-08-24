// отдельный файл ManagerConfiguration.cs
using Microsoft.EntityFrameworkCore;

public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Manager> builder)
    {
        builder.ToTable("Managers");
    }
}