using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Data.Config
{
    public class InstructorSubjectConfiguration : IEntityTypeConfiguration<InstructorSubject>
    {
        public void Configure(EntityTypeBuilder<InstructorSubject> builder)
        {
            builder.HasKey(ns => new { ns.InstID, ns.SubID });

            builder.HasOne(ns => ns.Instructor)
                .WithMany(ns => ns.InstructorSubjects)
                .HasForeignKey(ns => ns.InstID);

            builder.HasOne(ns => ns.Subject)
                .WithMany(ns => ns.InstructorSubjects)
                .HasForeignKey(ns => ns.SubID);
        }
    }
}
