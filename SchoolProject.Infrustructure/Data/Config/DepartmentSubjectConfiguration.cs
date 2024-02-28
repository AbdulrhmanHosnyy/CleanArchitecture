using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Data.Config
{
    public class DepartmentSubjectConfiguration : IEntityTypeConfiguration<DepartmentSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmentSubject> builder)
        {
            builder.HasKey(ds => new { ds.DID, ds.SubID });

            builder.HasOne(ds => ds.Department)
                .WithMany(s => s.DepartmentSubjects)
                .HasForeignKey(ds => ds.DID);

            builder.HasOne(ds => ds.Subject)
                .WithMany(s => s.DepartmentsSubjects)
                .HasForeignKey(ds => ds.SubID);
        }
    }
}
