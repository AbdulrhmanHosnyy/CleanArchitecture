using SchoolProject.Core.Features.Departments.Queries.Responses;
using SchoolProject.Data.Entities;
namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIdMapping()
        {
            CreateMap<Department, GetDepartmentByIdResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src
                    .Localize(src.DNameAr!, src.DNameEn!)))
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Instructor!
                    .Localize(src.Instructor.INameAr!, src.Instructor.INameEn!)))
                //.ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students))
                .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.DepartmentSubjects))
                .ForMember(dest => dest.Instructors, opt => opt.MapFrom(src => src.Instructors));

            //CreateMap<Student, GetDepartmentByIdStudentsResponse>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src
            //        .Localize(src.NameAr!, src.NameEn!)));

            CreateMap<DepartmentSubject, GetDepartmentByIdSubjectsResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject!
                    .Localize(src.Subject.SubjectNameAr!, src.Subject.SubjectNameEn!)));

            CreateMap<Instructor, GetDepartmentByIdInstructorsResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InstID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src
                    .Localize(src.INameAr!, src.INameEn!)));

        }
    }
}
