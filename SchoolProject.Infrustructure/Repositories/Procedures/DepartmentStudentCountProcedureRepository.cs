using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Infrustructure.Abstracts.Procedures;
using SchoolProject.Infrustructure.Data;
using StoredProcedureEFCore;

namespace SchoolProject.Infrustructure.Repositories.Procedures
{
    public class DepartmentStudentCountProcedureRepository : IDepartmentStudentCountProcedureRepository
    {
        #region Fields
        private readonly AppDbContext _appDbContext;
        #endregion
        #region Constructors
        public DepartmentStudentCountProcedureRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region Functions
        public async Task<IReadOnlyList<DepartmentStudentCountProcedure>> GetDepartmentStudentCountProcedure(DepartmentStudentCountProcedureParameters parameters)
        {
            var rows = new List<DepartmentStudentCountProcedure>();
            await _appDbContext.LoadStoredProc(nameof(DepartmentStudentCountProcedure))
                .AddParam(nameof(DepartmentStudentCountProcedureParameters.DID), parameters.DID)
                .ExecAsync(async r => rows = await r.ToListAsync<DepartmentStudentCountProcedure>());

            return rows;
        }
        #endregion

    }
}
