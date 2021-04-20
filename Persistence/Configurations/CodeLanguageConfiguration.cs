using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    class CodeLanguageConfiguration : IEntityTypeConfiguration<CodeLanguage>
    {
        public void Configure(EntityTypeBuilder<CodeLanguage> builder)
        {
            builder.HasOne(d => d.Code)
                   .WithMany(p => p.CodesLanguages)
                   .HasForeignKey(d => d.CodeId)
                   .HasConstraintName("FK_CodesLanguages_Codes");

            builder.HasOne(d => d.Language)
                   .WithMany(p => p.CodesLanguages)
                   .HasForeignKey(d => d.LanguageId)
                   .HasConstraintName("FK_CodesLanguages_Languages");
        }
    }
}
