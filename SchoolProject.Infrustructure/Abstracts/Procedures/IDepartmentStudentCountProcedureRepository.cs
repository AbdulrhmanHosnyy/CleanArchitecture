using SchoolProject.Data.Entities.Procedures;

namespace SchoolProject.Infrustructure.Abstracts.Procedures
{
    public interface IDepartmentStudentCountProcedureRepository
    {
        public Task<IReadOnlyList<DepartmentStudentCountProcedure>>
            GetDepartmentStudentCountProcedure(DepartmentStudentCountProcedureParameters parameters);
    }
}
