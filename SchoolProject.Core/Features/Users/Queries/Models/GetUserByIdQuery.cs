using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Users.Queries.Responses;

namespace SchoolProject.Core.Features.Users.Queries.Models
{
    public record GetUserByIdQuery(int Id) : IRequest<Response<GetUserByIdResponse>>;

}
