using Microsoft.AspNetCore.Http;
using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstracts
{
    public interface IInstructorService
    {
        public Task<decimal> GetInstructorSalarySummation();
        public Task<bool> IsNameArExist(string name);
        public Task<bool> IsNameEnExist(string name);
        public Task<bool> IsNameArExistExcludeSelf(string name, int id);
        public Task<bool> IsNameEnExistExcludeSelf(string name, int id);
        public Task<string> AddInstructorAsync(Instructor instructor, IFormFile file);
    }
}
