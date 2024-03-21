using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Data.Entities.Views;

namespace SchoolProject.Service.Abstracts
{
    public interface IDepartmentService
    {
        public Task<Department> GetDepartmentByIdAsync(int id);
        public Task<bool> IsDepartmentExist(int id);
        public Task<List<ViewDepartment>> GetViewDepartmentData();

        public Task<IReadOnlyList<DepartmentStudentCountProcedure>>
            GetDepartmentStudentCountProcedure(DepartmentStudentCountProcedureParameters parameters);
    }
}
