using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructors
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        #endregion

        #region Functions
        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentRepository.GetTableNoTracking().Where(d => d.DID == id)
                       .Include(d => d.Instructor)
                       .Include(d => d.DepartmentSubjects).ThenInclude(ds => ds.Subject)
                       .Include(d => d.Instructors)
                       .FirstOrDefaultAsync();

            return department!;
        }

        public async Task<bool> IsDepartmentExist(int id)
        {
            return await _departmentRepository.GetTableNoTracking().AnyAsync(d => d.DID.Equals(id));
        }
        #endregion

    }
}
