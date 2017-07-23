using System.Threading.Tasks;

namespace TeamUp.Core.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
