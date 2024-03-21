using SchoolProject.Infrustructure.InfrustructureBases;

namespace SchoolProject.Infrustructure.Abstracts.Views
{
    public interface IViewRepository<T> : IGenericRepository<T> where T : class
    {

    }
}
