using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Emails.Commands.Models
{
    public record SendEmailCommand(string Email, string Message) : IRequest<Response<string>>;

}
