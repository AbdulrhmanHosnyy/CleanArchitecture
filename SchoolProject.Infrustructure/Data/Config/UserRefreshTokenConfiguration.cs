using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrustructure.Data.Config
{
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.User)
                .WithMany(u => u.UserRefreshTokens)
                .HasForeignKey(u => u.UserId);
        }
    }
}
