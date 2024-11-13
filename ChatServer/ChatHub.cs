using Microsoft.AspNetCore.SignalR;

namespace ChatServer
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, HashSet<string>> RoomUsers = new Dictionary<string, HashSet<string>>();
        private static Dictionary<string, string> ConnectionSessions = new Dictionary<string, string>();

        public async Task JoinRoom(string roomName, string userName, string customSession)
        {
            if (!RoomUsers.ContainsKey(roomName))
            {
                RoomUsers[roomName] = new HashSet<string>();
            }

            // Если пользователь с таким CustomSession уже в комнате, не добавляем его повторно
            if (!RoomUsers[roomName].Contains(customSession))
            {
                RoomUsers[roomName].Add(customSession);
                ConnectionSessions[Context.ConnectionId] = customSession;

                await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

                // Обновляем количество пользователей
                await Clients.Group(roomName).SendAsync("UpdateUserCount", RoomUsers[roomName].Count);
            }
        }

        public async Task SendMessageToRoom(string roomName, string user, string message)
        {
            await Clients.Group(roomName).SendAsync("ReceiveMessage", user, message);
        }

        public async Task LeaveRoom(string roomName, string userName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("ReceiveMessage", "System", $"{userName} вышел с урока.");
        }


        private static int _connectedUsers = 0;

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string customSession;

            if (ConnectionSessions.TryGetValue(Context.ConnectionId, out customSession))
            {
                // Находим комнату, где находится пользователь с таким customSession
                foreach (var room in RoomUsers.Keys)
                {
                    if (RoomUsers[room].Remove(customSession))
                    {
                        await Clients.Group(room).SendAsync("UpdateUserCount", RoomUsers[room].Count);
                        break;
                    }
                }

                // Удаляем связь между ConnectionId и CustomSession
                ConnectionSessions.Remove(Context.ConnectionId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
