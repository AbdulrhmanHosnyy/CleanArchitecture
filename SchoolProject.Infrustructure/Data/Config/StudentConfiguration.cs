using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Data.Config
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.StudID);
            builder.Property(s => s.NameEn).HasMaxLength(500);
            builder.Property(s => s.Address).HasMaxLength(500);
            builder.Property(s => s.Phone).HasMaxLength(500);
            builder.HasOne(s => s.Department).WithMany(s => s.Students).HasForeignKey(s => s.DID);
        }
    }
}
