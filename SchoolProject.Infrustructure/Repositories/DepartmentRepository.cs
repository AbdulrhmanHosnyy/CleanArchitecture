using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;

namespace SchoolProject.Infrustructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        #region Fields
        private DbSet<Department> _departments;
        #endregion

        #region Consturctors
        public DepartmentRepository(AppDbContext context) : base(context)
        {
            _departments = context.Set<Department>();
        }
        #endregion

        #region Functions
        #endregion
    }
}
