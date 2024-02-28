using AutoMapper;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {
            GetStudentsListMapping();
            GetStudentByIdMapping();
            AddStudentCommandMapping();
            EditStudentCommandMapping();
        }
    }
}
