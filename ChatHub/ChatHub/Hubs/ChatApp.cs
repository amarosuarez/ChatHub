using Microsoft.AspNetCore.SignalR;
using Models;

namespace ChatHub.Hubs
{
    public class ChatApp : Hub
    {
        public async Task SendMessage(MensajeUsuario mensajeUsuario)
        {
            await Clients.All.SendAsync("ReceiveMessage", mensajeUsuario);
        }
    }
}
