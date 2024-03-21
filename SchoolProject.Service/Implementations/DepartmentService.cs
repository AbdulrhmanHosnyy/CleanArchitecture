using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Abstracts.Procedures;
using SchoolProject.Infrustructure.Abstracts.Views;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IViewRepository<ViewDepartment> _viewRepository;
        private readonly IDepartmentStudentCountProcedureRepository _departmentStudentCountProcedureRepository;
        #endregion

        #region Constructors
        public DepartmentService(IDepartmentRepository departmentRepository, IViewRepository<ViewDepartment> viewRepository,
            IDepartmentStudentCountProcedureRepository departmentStudentCountProcedureRepository)
        {
            _departmentRepository = departmentRepository;
            _viewRepository = viewRepository;
            _departmentStudentCountProcedureRepository = departmentStudentCountProcedureRepository;
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

        public async Task<List<ViewDepartment>> GetViewDepartmentData()
        {
            var viewDepartment = await _viewRepository.GetTableNoTracking().ToListAsync();
            return viewDepartment;
        }

        public async Task<bool> IsDepartmentExist(int id)
        {
            return await _departmentRepository.GetTableNoTracking().AnyAsync(d => d.DID.Equals(id));
        }

        public async Task<IReadOnlyList<DepartmentStudentCountProcedure>>
            GetDepartmentStudentCountProcedure(DepartmentStudentCountProcedureParameters parameters) =>
                await _departmentStudentCountProcedureRepository.GetDepartmentStudentCountProcedure(parameters);


        #endregion

    }
}
