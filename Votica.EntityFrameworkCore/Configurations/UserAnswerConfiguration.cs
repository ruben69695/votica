using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Votica.Domain;

namespace Votica.EntityFrameworkCore.Configurations
{
    public class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswer>
    {
        public void Configure(EntityTypeBuilder<UserAnswer> builder)
        {
            builder.ToTable("answersPerUser")
                .HasKey(po => new { po.UserEmail, po.AnswerId });
            
            builder.HasOne(po => po.Answer)
                .WithMany(o => o.UsersAnswers)
                .HasForeignKey(po => po.AnswerId);

            builder.HasOne(po => po.User)
                .WithMany(p => p.QuestionAnswers)
                .HasForeignKey(po => po.UserEmail);
                
        }
    }
}