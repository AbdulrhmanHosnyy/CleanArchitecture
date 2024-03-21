using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrustructure.Abstracts.Views;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;

namespace SchoolProject.Infrustructure.Repositories.Views
{
    public class ViewDepartmentRepository : GenericRepository<ViewDepartment>, IViewRepository<ViewDepartment>
    {
        #region Fields
        private readonly DbSet<ViewDepartment> _viewDepartment;
        #endregion

        #region Consturctors
        public ViewDepartmentRepository(AppDbContext context) : base(context)
        {
            _viewDepartment = context.Set<ViewDepartment>();
        }
        #endregion

        #region Functions
        #endregion
    }
}
