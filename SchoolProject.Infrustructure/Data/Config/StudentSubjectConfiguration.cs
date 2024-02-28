using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Data.Config
{
    public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(ss => new { ss.StudID, ss.SubID });

            builder.HasOne(ss => ss.Student)
                .WithMany(ss => ss.StudentSubjects)
                .HasForeignKey(ss => ss.StudID);

            builder.HasOne(ss => ss.Subject)
                .WithMany(ss => ss.StudentsSubjects)
                .HasForeignKey(ss => ss.SubID);
        }
    }
}
