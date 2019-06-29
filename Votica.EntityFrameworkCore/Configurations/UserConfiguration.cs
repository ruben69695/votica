using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Votica.Domain;

namespace Votica.EntityFrameworkCore.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users")
                .HasKey(p => p.Email);

            builder.Property(p => p.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(60)")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(45)")
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasColumnName("lastname")
                .HasColumnType("varchar(90)")
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnName("lastname")
                .HasColumnType("varchar(90)")
                .IsRequired();

            builder.Property(u => u.Salt)
                .HasColumnName("salt")
                .HasColumnType("varchar(16)")
                .IsRequired();
                
        }
    }
}