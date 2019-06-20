using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Votica.Domain;

namespace Votica.EntityFrameworkCore.Configurations
{
    public class OptionConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.ToTable("options")
                .HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            
            builder.Property(o => o.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(60)")
                .HasMaxLength(60)
                .IsRequired();
                
        }
    }
}