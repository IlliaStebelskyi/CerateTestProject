using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Room");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("RoomId");

        builder.HasMany(c => c.RoomTags)
            .WithOne(c => c.Room)
            .HasForeignKey(c => c.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Bookings)
            .WithOne(c => c.Room)
            .HasForeignKey(c => c.RoomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}