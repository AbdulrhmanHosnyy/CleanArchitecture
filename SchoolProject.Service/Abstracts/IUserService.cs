using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Abstracts
{
    public interface IUserService
    {
        public Task<string> AddUserAsync(User user, string password);
    }
}
