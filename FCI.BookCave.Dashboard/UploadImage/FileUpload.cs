

namespace FCI.BookCave.Dashboard.UploadImage
{
    public class FileUpload : IFileUpload
    {
        IWebHostEnvironment _env;

        public FileUpload(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadFileAsync(string filePath, IFormFile file)
        {
            string uploadFolder = _env.WebRootPath + filePath;
            if (!Directory.Exists(uploadFolder)) // validation for the folder 
            {
                Directory.CreateDirectory(uploadFolder);
            }
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string FullImagePath = Path.Combine(uploadFolder, uniqueFileName);
            using (var stream = new FileStream(FullImagePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Dispose();
            }
            return Path.Combine(filePath, uniqueFileName);
        }
    }
}
