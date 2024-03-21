using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrustructure.Abstracts.Functions;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Service.Abstracts;
using System.Data;

namespace SchoolProject.Service.Implementations
{
    public class InstructorService : IInstructorService
    {
        #region Fields
        private readonly AppDbContext _appDbContext;
        private readonly IInstructorFunctionsRepository _functionsRepository;
        #endregion

        #region Constructors
        public InstructorService(AppDbContext appDbContext, IInstructorFunctionsRepository functionsRepository)
        {
            _appDbContext = appDbContext;
            _functionsRepository = functionsRepository;
        }
        #endregion

        #region Functions
        public async Task<decimal> GetInstructorSalarySummation()
        {
            decimal result = 0;
            using (var cmd = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                result = await _functionsRepository.GetInstructorSalarySummation("select * from dbo.GetInstructorData()", cmd);
            }
            return result;
        }
        #endregion

    }
}
