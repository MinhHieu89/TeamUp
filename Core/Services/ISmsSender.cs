using System.Threading.Tasks;

namespace TeamUp.Core.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
