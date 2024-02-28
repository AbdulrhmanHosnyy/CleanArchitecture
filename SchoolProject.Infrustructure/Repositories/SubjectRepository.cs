using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;

namespace SchoolProject.Infrustructure.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        #region Fields
        private DbSet<Subject> _subjects;
        #endregion

        #region Consturctors
        public SubjectRepository(AppDbContext context) : base(context)
        {
            _subjects = context.Set<Subject>();
        }
        #endregion

        #region Functions
        #endregion
    }
}
