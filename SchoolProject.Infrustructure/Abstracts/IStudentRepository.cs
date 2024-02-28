using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.InfrustructureBases;

namespace SchoolProject.Infrustructure.Abstracts
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        public Task<List<Student>> GetStudentsAsync();
    }
}
