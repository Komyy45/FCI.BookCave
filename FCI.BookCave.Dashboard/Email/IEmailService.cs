namespace FCI.BookCave.Dashboard.Email
{
    public interface IEmailService
    {
        Task sendAsync(string email, string subject, string content);
    }
}
