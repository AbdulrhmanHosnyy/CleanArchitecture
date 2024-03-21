using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Abstracts.Functions;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Service.Abstracts;
using System.Data;

namespace SchoolProject.Service.Implementations
{
    public class InstructorService : IInstructorService
    {
        #region Fields
        private readonly AppDbContext _appDbContext;
        private readonly IInstructorFunctionsRepository _functionsRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _contextAccessor;
        #endregion

        #region Constructors
        public InstructorService(AppDbContext appDbContext, IInstructorFunctionsRepository functionsRepository,
            IInstructorRepository instructorRepository, IFileService fileService, IHttpContextAccessor contextAccessor)
        {
            _appDbContext = appDbContext;
            _functionsRepository = functionsRepository;
            _instructorRepository = instructorRepository;
            _fileService = fileService;
            _contextAccessor = contextAccessor;
        }
        #endregion

        #region Functions
        public async Task<decimal> GetInstructorSalarySummation()
        {
            decimal result = 0;
            using (var cmd = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                result = await _functionsRepository.GetInstructorSalarySummation("select * from dbo.GetInstructorData()", cmd);
            }
            return result;
        }
        public async Task<bool> IsNameArExist(string name)
        {
            var instructor = _instructorRepository.GetTableNoTracking().Where(s => s.INameAr == name).FirstOrDefault();
            return instructor != null ? true : false;
        }
        public async Task<bool> IsNameEnExist(string name)
        {
            var instructor = _instructorRepository.GetTableNoTracking().Where(s => s.INameEn == name).FirstOrDefault();
            return instructor != null ? true : false;
        }
        public async Task<bool> IsNameArExistExcludeSelf(string name, int id)
        {
            var instructor = await _instructorRepository.GetTableNoTracking()
                .Where(s => s.INameAr == name && s.InstID != id)
                .FirstOrDefaultAsync();
            return instructor != null ? true : false;
        }
        public async Task<bool> IsNameEnExistExcludeSelf(string name, int id)
        {
            var instructor = await _instructorRepository.GetTableNoTracking()
                .Where(s => s.INameEn == name && s.InstID != id)
                .FirstOrDefaultAsync();
            return instructor != null ? true : false;
        }

        public async Task<string> AddInstructorAsync(Instructor instructor, IFormFile file)
        {
            var context = _contextAccessor.HttpContext.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var imageUrl = await _fileService.UploadImage("Instructors", file);
            switch (imageUrl)
            {
                case "NoImage": return "NoImage"; break;
                case "FailedToUploadImage": return "FailedToUploadImage"; break;
            }
            instructor.Image = baseUrl + imageUrl;
            try
            {
                var result = _instructorRepository.AddAsync(instructor);
                return "Success";
            }
            catch (Exception)
            {

                return "Failed";
            }

        }
        #endregion

    }
}
