using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Responses;

namespace SchoolProject.Core.Features.Departments.Queries.Models
{
    public record GetDepartmentStudentCountQuery : IRequest<Response<List<GetDepartmentStudentCountResponse>>>;
}
