using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Votica.Domain;

namespace Votica.EntityFrameworkCore.Configurations
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable("participants")
                .HasKey(p => p.Email);

            builder.Property(p => p.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(60)")
                .ValueGeneratedOnAdd();
                
        }
    }
}