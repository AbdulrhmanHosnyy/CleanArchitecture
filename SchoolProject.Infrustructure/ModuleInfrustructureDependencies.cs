using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Abstracts.Functions;
using SchoolProject.Infrustructure.Abstracts.Procedures;
using SchoolProject.Infrustructure.Abstracts.Views;
using SchoolProject.Infrustructure.InfrustructureBases;
using SchoolProject.Infrustructure.Repositories;
using SchoolProject.Infrustructure.Repositories.Functions;
using SchoolProject.Infrustructure.Repositories.Procedures;
using SchoolProject.Infrustructure.Repositories.Views;

namespace SchoolProject.Infrustructure
{
    public static class ModuleInfrustructureDependencies
    {
        public static IServiceCollection AddInfrustructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IInstructorRepository, InstructorRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //  Views
            services.AddTransient<IViewRepository<ViewDepartment>, ViewDepartmentRepository>();

            //  Procedures
            services.AddTransient<IDepartmentStudentCountProcedureRepository, DepartmentStudentCountProcedureRepository>();

            //  Procedures
            services.AddTransient<IInstructorFunctionsRepository, InstructorFunctionsRepository>();
            return services;
        }
    }
}
