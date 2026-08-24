using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tag");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("TagId");

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(t => t.Name).IsUnique();
    }
}