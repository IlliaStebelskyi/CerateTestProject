using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RoomTagConfiguration : IEntityTypeConfiguration<RoomTag>
{
    public void Configure(EntityTypeBuilder<RoomTag> builder)
    {
        builder.ToTable("RoomTags");
        builder.HasKey(c => new { c.RoomId, c.TagId});
        builder.Property(c => c.TagId).HasColumnName("TagId");

        builder.HasOne(c => c.Room)
            .WithMany(c => c.RoomTags)
            .HasForeignKey(c => c.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Tag)
            .WithMany(t => t.RoomTags)
            .HasForeignKey(c => c.TagId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}