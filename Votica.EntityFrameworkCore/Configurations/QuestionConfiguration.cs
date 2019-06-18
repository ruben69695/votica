using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Votica.Domain;

namespace Votica.EntityFrameworkCore.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("questions")
                .HasKey(q => q.Id)
                .HasName("id");
            
            builder.Property(q => q.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(150)")
                .HasMaxLength(150)
                .IsRequired();

            builder.HasMany(q => q.Options)
                .WithOne(option => option.Question);
            
        }
    }
}