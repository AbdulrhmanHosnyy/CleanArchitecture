using SchoolProject.Core.Features.Departments.Queries.Responses;
using SchoolProject.Data.Entities.Views;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentStudentCountMapper()
        {
            CreateMap<ViewDepartment, GetDepartmentStudentCountResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
                .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.StudentCount));
        }
    }
}
