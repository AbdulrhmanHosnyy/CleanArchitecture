using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities
{
    public class Subject : GeneralLocalizableEntity
    {
        public Subject()
        {
            StudentsSubjects = new HashSet<StudentSubject>();
            DepartmentsSubjects = new HashSet<DepartmentSubject>();
            InstructorSubjects = new HashSet<InstructorSubject>();
        }
        public int SubID { get; set; }
        public string? SubjectNameAr { get; set; }
        public string? SubjectNameEn { get; set; }
        public int? Period { get; set; }
        public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
        public virtual ICollection<DepartmentSubject> DepartmentsSubjects { get; set; }
        public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; }
    }
}
