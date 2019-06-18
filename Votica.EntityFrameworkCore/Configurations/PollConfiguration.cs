using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Votica.Domain;

namespace Votica.EntityFrameworkCore.Configurations
{
    public class PollConfiguration : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.ToTable("polls")
                .HasKey(p => p.Id)
                .HasName("id");
            
            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(150)")
                .HasMaxLength(150)
                .IsRequired();
            
            builder.Property(p => p.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);
            
            builder.Property(p => p.CreationDate)
                .HasColumnName("creationDate")
                .HasColumnType("datetime")
                .IsRequired();
            
            builder.Property(p => p.ExpirationDate)
                .HasColumnName("expirationDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasMany(poll => poll.Questions)
                .WithOne(question => question.Poll)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}