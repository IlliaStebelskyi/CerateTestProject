using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Booking", t =>
            t.HasCheckConstraint("CK_Booking_EndAfterStart", "\"EndTime\" > \"StartTime\""));

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasColumnName("BookingId");

        builder.HasOne(b => b.Room)
            .WithMany(c => c.Bookings)
            .HasForeignKey(c => c.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.User)
            .WithMany(c => c.Bookings)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(b => new { b.RoomId, b.StartTime })
            .IsUnique();

        builder.Property(b => b.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

    }
}

// HasOne / WithMany / WithOne     — направление связи
// HasForeignKey / HasForeignKey<T> — где лежит FK
// OnDelete(DeleteBehavior.X)      — Cascade / Restrict / SetNull
// HasKey                          — PK (в т.ч. составной через new { })
// Property().HasMaxLength()       — ограничение длины
// Property().IsRequired()         — NOT NULL
// HasIndex().IsUnique()           — уникальность
// OwnsOne                         — value object (Address)
// HasConversion<string>()         — enum как строка
// IsRowVersion()                  — concurrency token
// HasQueryFilter                  — soft delete / global filter
// HasDiscriminator                — TPH-наследование
// ToTable                         — имя таблицы + check constraints
// ApplyConfigurationsFromAssembly — подключение всех конфигов разом