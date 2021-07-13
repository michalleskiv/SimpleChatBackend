using System.Threading.Tasks;
using SimpleChat.Models;

namespace SimpleChat.Hubs.Interfaces
{
    public interface IChatHub
    {
        Task DisplayMessage(string message);
        Task UpdateProgressBar(int percentage);
        Task DisplayProgressMessage(string message);
        Task ReceiveMessage(Message message);
    }
}