using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ReversiMvcApp.Hubs
{
    public class GameHub : Hub
    {
        public Task Join(string gameToken)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, $"game_{gameToken}");
        }

        public Task Leave(string gameToken)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, $"game_{gameToken}");
        }

        public async Task SendMessage(string gameToken, string message, bool join)
        {
            if (join)
            {
                await Join(gameToken);
            }
            else
            {
                await Clients.Groups($"game_{gameToken}").SendAsync("ReceiveMessage", gameToken, message, false);
            }
        }
    }
}
