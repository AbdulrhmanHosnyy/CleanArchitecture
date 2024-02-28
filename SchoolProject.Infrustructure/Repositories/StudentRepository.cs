using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        #region Fields
        private readonly DbSet<Student> _students;
        #endregion

        #region Consturctors
        public StudentRepository(AppDbContext context) : base(context) 
        { 
            _students = context.Set<Student>();
        }
        #endregion

        #region Functions Handling
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _students.Include(s => s.Department).ToListAsync();
        }
        #endregion

    }
}
