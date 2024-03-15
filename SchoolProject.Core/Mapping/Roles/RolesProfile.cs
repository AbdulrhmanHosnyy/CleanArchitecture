using AutoMapper;

namespace SchoolProject.Core.Mapping.Roles
{
    public partial class RolesProfile : Profile
    {
        public RolesProfile()
        {
            GetRolesListMapping();
            GetRoleByIdMapping();
        }
    }
}
