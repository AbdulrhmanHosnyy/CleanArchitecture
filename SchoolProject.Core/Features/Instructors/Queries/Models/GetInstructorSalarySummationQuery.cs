using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Instructors.Queries.Models
{
    public record GetInstructorSalarySummationQuery : IRequest<Response<decimal>>;

}
