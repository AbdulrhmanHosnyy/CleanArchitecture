using SchoolProject.Data.Results;
using SchoolProject.Infrustructure.Abstracts.Functions;
using SchoolProject.Infrustructure.Data;
using StoredProcedureEFCore;
using System.Data.Common;

namespace SchoolProject.Infrustructure.Repositories.Functions
{
    public class InstructorFunctionsRepository : IInstructorFunctionsRepository
    {
        #region Fields
        private readonly AppDbContext _appDbContext;
        #endregion
        #region Constructors
        public InstructorFunctionsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion
        #region Functions
        public async Task<decimal> GetInstructorSalarySummation(string query, DbCommand cmd)
        {
            decimal response = 0;
            cmd.CommandText = query;
            //var value = cmd.ExecuteScalar();
            var reader = await cmd.ExecuteReaderAsync();
            var value = await reader.ToListAsync<GetInstructorDataFunctionResult>();
            var result = reader.ToString();
            //var result = value.ToString();
            if (decimal.TryParse(result, out decimal d)) response = d;
            return response;
        }
        #endregion
    }
}
