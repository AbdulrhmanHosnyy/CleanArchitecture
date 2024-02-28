using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities
{
    public partial class Department : GeneralLocalizableEntity
    {
        public Department()
        {
            Students = new HashSet<Student>();
            Instructors = new HashSet<Instructor>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
        }

        public int DID { get; set; }
        public string? DNameAr { get; set; }
        public string? DNameEn { get; set; }
        public int? InstructorManager { get; set; }
        public virtual Instructor? Instructor { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
    }
}
