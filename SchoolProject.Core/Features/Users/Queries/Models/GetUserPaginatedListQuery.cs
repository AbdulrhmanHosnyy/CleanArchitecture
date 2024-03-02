using MediatR;
using SchoolProject.Core.Features.Users.Queries.Responses;
using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.Users.Queries.Models
{
    public record GetUserPaginatedListQuery(int PageNumber, int PageSize) :
        IRequest<PaginatedResult<GetUserPaginatedListResponse>>;

}
