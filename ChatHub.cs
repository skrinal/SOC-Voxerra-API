using Microsoft.AspNetCore.SignalR;

namespace Voxxera_backend
{
    public class ChatHub : Hub
    {
        // Method for sending messages to all connected clients
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        // Method for live note updates
        public async Task UpdateNote(string noteContent)
        {
            await Clients.All.SendAsync("ReceiveNoteUpdate", noteContent);
        }
    }
}
