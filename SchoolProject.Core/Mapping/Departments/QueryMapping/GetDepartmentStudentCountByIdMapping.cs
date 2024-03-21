using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Departments.Queries.Responses;
using SchoolProject.Data.Entities.Procedures;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentStudentCountByIdMapping()
        {
            CreateMap<GetDepartmentStudentCountByIdQuery, DepartmentStudentCountProcedureParameters>();

            CreateMap<DepartmentStudentCountProcedure, GetDepartmentStudentCountByIdResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
                .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.StudentCount));
        }
    }
}
