using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;

namespace SchoolProject.Infrustructure.Repositories
{
    public class InstructorRepository : GenericRepository<Instructor>, IInstructorRepository
    {
        #region Fields
        private DbSet<Instructor> _instructors;
        #endregion

        #region Consturctors
        public InstructorRepository(AppDbContext context) : base(context)
        {
            _instructors = context.Set<Instructor>();
        }
        #endregion

        #region Functions
        #endregion
    }
}
