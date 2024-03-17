using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Queries.Models
{
    public record ConfirmEmailQuery(int UserId, string Code) : IRequest<Response<string>>;
}
