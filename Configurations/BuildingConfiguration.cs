using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.ToTable("Buildings");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("BuildingId");

        builder.HasMany(c => c.Rooms)
            .WithOne(c => c.Building)
            .HasForeignKey(c => c.BuildingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(c => c.Address, a =>
        {
            a.Property(c => c.Street).HasMaxLength(200);
            a.Property(c => c.City).HasMaxLength(100);
            a.Property(x => x.PostalCode).HasMaxLength(20);
        });
    }
}