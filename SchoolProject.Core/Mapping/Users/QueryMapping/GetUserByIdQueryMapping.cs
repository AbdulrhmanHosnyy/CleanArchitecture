using SchoolProject.Core.Features.Users.Queries.Responses;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.Users
{
    public partial class UserProfile
    {
        public void GetUserByIdQueryMapping()
        {
            CreateMap<User, GetUserByIdResponse>();
        }
    }
}
