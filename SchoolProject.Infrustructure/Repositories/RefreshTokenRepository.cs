using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;

namespace SchoolProject.Infrustructure.Repositories
{
    public class RefreshTokenRepository : GenericRepository<UserRefreshToken>, IRefreshTokenRepository
    {
        #region Fields
        private DbSet<UserRefreshToken> _users;
        #endregion
        #region Construcotrs
        #endregion
        #region Functions
        #endregion
        public RefreshTokenRepository(AppDbContext context) : base(context)
        {
            _users = context.Set<UserRefreshToken>();
        }
    }
}
