using ChatAPI.DataService;
using ChatAPI.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Hubs
{
    public class ChatHub: Hub
    {
        private readonly SharedDB _shared;

        public ChatHub(SharedDB shared) => _shared = shared;

        public async Task JoinChat(UserConnection conn) 
        {
            await Clients.All.SendAsync("JoinChat", "admin", $"{conn.Username} has joined");
        }

        public async Task JoinSpecificChatRoom(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);

            _shared.connections[Context.ConnectionId] = conn;

            await Clients.Group(conn.ChatRoom).SendAsync("JoinSpecificChatRoom", "admin", $"{conn.Username} has joined to room {conn.ChatRoom}");
        }

        public async Task SendMessage(string msg)
        {
            if (_shared.connections.TryGetValue(Context.ConnectionId, out UserConnection conn)) 
            {
                await Clients.Group(conn.ChatRoom).SendAsync("ReceiveSpecificMessage", conn.Username, msg);
            }
        }
    }
}
