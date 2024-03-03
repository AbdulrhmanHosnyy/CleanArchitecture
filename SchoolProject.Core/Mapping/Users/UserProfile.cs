using AutoMapper;

namespace SchoolProject.Core.Mapping.Users
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            AddUserCommandMapping();
            GetUserPaginatedListQueryMapping();
            GetUserByIdQueryMapping();
            EditUserCommandMapping();
        }
    }
}
