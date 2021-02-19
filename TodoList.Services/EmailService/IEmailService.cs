using System.Threading.Tasks;

namespace TodoList.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string ToEmail, string FromName, string Subject, string Message);
    }
}
