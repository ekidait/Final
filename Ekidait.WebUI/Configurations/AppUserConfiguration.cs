using Ekidait.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ekidait.Data.Configurations
{
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(x => x.Surname).HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(x => x.Password).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(x => x.UserName).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50);
            builder.HasData(
                new AppUser
                {
                    Id = 1,
                    UserName = "Ekidait",
                    Email = "Dilaratqz@gmail.com",
                    IsActive = true,
                    IsAdmin = true,
                    Name = "Dilara",
                    Surname = "Topuz",
                    Password = "123456*"

                }
                );
        }
    }
}
