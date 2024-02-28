using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Data.Config
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(s => s.DID);
            builder.Property(s => s.DNameEn).HasMaxLength(500);

            builder.HasOne(i => i.Instructor)
                .WithOne(i => i.ManagedDepartment)
                .HasForeignKey<Department>(i => i.InstructorManager)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
