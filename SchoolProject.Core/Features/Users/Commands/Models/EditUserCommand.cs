using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Users.Commands.Models
{
    public record EditUserCommand(int Id, string FullName, string UserName, string Email, string? Address, string? Country,
        string? PhoneNumber) : IRequest<Response<string>>;
}
