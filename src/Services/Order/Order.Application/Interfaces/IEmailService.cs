using Order.Application.Models;
using System.Threading.Tasks;

namespace Order.Application.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}