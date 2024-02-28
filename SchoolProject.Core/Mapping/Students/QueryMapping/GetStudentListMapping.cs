using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentsListMapping()
        {
            CreateMap<Student, GetStudentsListResponse>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Localize(src.Department.DNameAr, src.Department.DNameEn)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));
        }
    }
}
