using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities
{
    public class Instructor : GeneralLocalizableEntity
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            InstructorSubjects = new HashSet<InstructorSubject>();
        }

        public int InstID { get; set; }
        public string? INameAr { get; set; }
        public string? INameEn { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public decimal? Salary { get; set; }
        public int? SupervisorId { get; set; }
        public virtual Instructor? Supervisor { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
        public virtual Department? ManagedDepartment { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; }


    }
}
