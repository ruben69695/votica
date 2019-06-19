using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Votica.Domain;

namespace Votica.EntityFrameworkCore.Configurations
{
    public class ParticipantOptionConfiguration : IEntityTypeConfiguration<ParticipantOption>
    {
        public void Configure(EntityTypeBuilder<ParticipantOption> builder)
        {
            builder.ToTable("optionsPerParticipant")
                .HasKey(po => new { po.ParticipantEmail, po.OptionId });
            
            builder.HasOne(po => po.Option)
                .WithMany(o => o.Participants)
                .HasForeignKey(po => po.OptionId);

            builder.HasOne(po => po.Participant)
                .WithMany(p => p.Options)
                .HasForeignKey(po => po.ParticipantEmail);
                
        }
    }
}