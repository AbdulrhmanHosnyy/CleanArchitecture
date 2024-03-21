using System.Data.Common;

namespace SchoolProject.Infrustructure.Abstracts.Functions
{
    public interface IInstructorFunctionsRepository
    {
        public Task<decimal> GetInstructorSalarySummation(string query, DbCommand cmd);
    }
}
