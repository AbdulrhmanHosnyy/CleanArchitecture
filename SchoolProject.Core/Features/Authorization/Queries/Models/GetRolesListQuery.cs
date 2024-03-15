using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Responses;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public record GetRolesListQuery() : IRequest<Response<List<GetRolesListResponse>>>;
}
