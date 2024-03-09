using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Queries.Models
{
    public record AuthorizeUserQuery(string AccesssToken) : IRequest<Response<string>>;

}
