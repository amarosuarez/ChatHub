using Microsoft.AspNetCore.SignalR;
using Models;

namespace ChatHub.Hubs
{
    public class ChatApp : Hub
    {
        public Task JoinRoom(string roomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task SendMessage(MensajeUsuario mensajeUsuario)
        {
            await Clients.Group(mensajeUsuario.Sala).SendAsync("ReceiveMessage", mensajeUsuario);
            //await Clients.All.SendAsync("ReceiveMessage", mensajeUsuario);
        }
    }
}
