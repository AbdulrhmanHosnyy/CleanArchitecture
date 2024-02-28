using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IStudentRepository _studentRepository;
        #endregion

        #region Constructors
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        #endregion

        #region Functions Handling
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentRepository.GetStudentsAsync();
        }
        public async Task<Student> GetStudentByIdWithIncludeAsync(int id)
        {
            //var student = await _studentRepository.GetByIdAsync(id);
            var student = _studentRepository.GetTableNoTracking()
                .Include(s => s.Department)
                .Where(s => s.StudID == id)
                .FirstOrDefault();

            return student;
        }
        public async Task<Student> GetByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return student;
        }
        public async Task<string> AddAsync(Student student)
        {

            await _studentRepository.AddAsync(student);
            return "Success";
        }
        public async Task<bool> IsNameArExist(string name)
        {
            var student = _studentRepository.GetTableNoTracking().Where(s => s.NameAr == name).FirstOrDefault();
            return student != null ? true : false;
        }
        public async Task<bool> IsNameEnExist(string name)
        {
            var student = _studentRepository.GetTableNoTracking().Where(s => s.NameEn == name).FirstOrDefault();
            return student != null ? true : false;
        }
        public async Task<bool> IsNameArExistExcludeSelf(string name, int id)
        {
            var student = await _studentRepository.GetTableNoTracking()
                .Where(s => s.NameAr == name && s.StudID != id)
                .FirstOrDefaultAsync();
            return student != null ? true : false;
        }
        public async Task<bool> IsNameEnExistExcludeSelf(string name, int id)
        {
            var student = await _studentRepository.GetTableNoTracking()
                .Where(s => s.NameEn == name && s.StudID != id)
                .FirstOrDefaultAsync();
            return student != null ? true : false;
        }
        public async Task<string> EditAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            return "Success";
        }
        public async Task<string> DeleteAsync(Student student)
        {
            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";
            }
            catch
            {
                await trans.RollbackAsync();
                return "Failed";
            }

        }
        public IQueryable<Student> GetStudentsQueryable()
        {
            return _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
        }
        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderBy, string search)
        {
            var querable = _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();

            if (search != null)
                querable = querable.Where(x => x.NameEn.Contains(search) || x.Address.Contains(search));

            switch (orderBy)
            {
                case StudentOrderingEnum.StudId:
                    querable = querable.OrderBy(s => s.StudID);
                    break;
                case StudentOrderingEnum.Name:
                    querable = querable.OrderBy(s => s.NameEn);
                    break;
                case StudentOrderingEnum.Address:
                    querable = querable.OrderBy(s => s.Address);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    querable = querable.OrderBy(s => s.Department.DNameEn);
                    break;
                default:
                    querable = querable.OrderBy(s => s.StudID);
                    break;
            }
            return querable;
        }

        public IQueryable<Student> GetStudentsByDepartmentIdQueryable(int departmentId)
        {
            return _studentRepository.GetTableNoTracking().Where(s => s.DID.Equals(departmentId)).AsQueryable();
        }
        #endregion
    }
}
