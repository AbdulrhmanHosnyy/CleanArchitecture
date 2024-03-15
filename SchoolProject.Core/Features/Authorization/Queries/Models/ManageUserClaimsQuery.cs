using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public record ManageUserClaimsQuery(int UserId) : IRequest<Response<ManageUserClaimsResponse>>;

}
