using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class FileService : IFileService
    {
        #region Fields
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Constructors
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Functions
        public async Task<string> UploadImage(string Location, IFormFile file)
        {
            var path = _webHostEnvironment.WebRootPath + "/" + Location + "/";
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extension;
            if (file.Length <= 0) return "NoImage";
            try
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                using (FileStream fileStream = File.Create(path + fileName))
                {
                    await file.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                    return $"/{Location}/{fileName}";
                }
            }
            catch (Exception)
            {

                return "FailedToUploadImage";
            }

        }
        #endregion
    }
}
