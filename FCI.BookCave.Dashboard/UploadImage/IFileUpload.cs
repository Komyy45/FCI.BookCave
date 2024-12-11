namespace FCI.BookCave.Dashboard.UploadImage
{
    public interface IFileUpload
    {
        Task<string> UploadFileAsync(string filePath, IFormFile file);
    }
}
