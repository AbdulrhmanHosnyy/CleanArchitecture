using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Queries.Models
{
    public record ConfirmResetPasswordQuery(string Code, string Email) : IRequest<Response<string>>;

}
