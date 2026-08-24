// отдельный файл RegularConfiguration.cs — у тебя его вообще нет
using Microsoft.EntityFrameworkCore;

public class RegularConfiguration : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Regular>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Regular> builder)
    {
        builder.ToTable("Regulars");
    }
}