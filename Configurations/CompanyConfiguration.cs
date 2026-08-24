using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Company");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("CompanyId");

        builder.HasMany(c => c.Users)
            .WithOne(u => u.Company)
            .HasForeignKey(u => u.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}


// HasOne(x => x.Nav)      // "у X есть один Y" — начало конфигурации связи
// WithMany(y => y.Coll)   // обратная сторона — коллекция; используется для One-to-Many
// WithOne(y => y.Nav)     // обратная сторона — один объект; используется для One-to-One
// HasForeignKey(x => x.FkProp)          // обычная сторона — где лежит FK
// HasForeignKey<T>(x => x.FkProp)       // для One-to-One — обязателен generic-тип
// HasMany(x => x.Coll)    // альтернативная точка входа — "у X есть много Y"
// WithMany()               // без аргумента — implicit M2M (без явной join-таблицы)
// IsRequired()             // связь обязательна (FK not null)
// IsRequired(false)        // связь необязательна (FK nullable)
// OnDelete(DeleteBehavior.X)  // поведение при удалении родителя
// HasPrincipalKey(x => x.Prop) // если FK ссылается не на PK, а на другое уникальное поле