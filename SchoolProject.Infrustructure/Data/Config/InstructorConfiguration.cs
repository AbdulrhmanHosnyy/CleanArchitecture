using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Data.Config
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(i => i.InstID);
            builder.Property(i => i.INameEn).HasMaxLength(500);
            builder.Property(i => i.INameAr).HasMaxLength(500);

            builder.HasOne(i => i.Department)
                .WithMany(i => i.Instructors)
                .HasForeignKey(i => i.DepartmentId);

            builder.HasOne(i => i.Supervisor)
                .WithMany(i => i.Instructors)
                .HasForeignKey(i => i.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
