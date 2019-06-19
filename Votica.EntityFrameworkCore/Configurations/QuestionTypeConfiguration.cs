using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Votica.Domain;

namespace Votica.EntityFrameworkCore.Configurations
{
    public class QuestionTypeConfiguration : IEntityTypeConfiguration<QuestionType>
    {
        public void Configure(EntityTypeBuilder<QuestionType> builder)
        {
            builder.ToTable("questionTypes")
                .HasKey(qt => qt.Id)
                .HasName("id");
            
            builder.Property(qt => qt.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(35)")
                .HasMaxLength(35)
                .IsRequired();

            builder.HasMany(qt => qt.Questions)
                .WithOne(question => question.Type)
                .IsRequired();
            
        }
    }
}