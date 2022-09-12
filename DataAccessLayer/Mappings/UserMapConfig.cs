using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Mappings
{
    public class UserMapConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USUARIOS");
            builder.Property(p => p.Username).IsRequired().HasMaxLength(100).IsUnicode(false);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100).IsUnicode(false);
            builder.Property(p => p.Password).IsRequired().HasMaxLength(20).IsUnicode(false);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(100).IsUnicode(false);

            builder.HasIndex(p => p.Username).IsUnique().HasDatabaseName("UQ_USUARIOS_USERNAME");
        }
    }
}

