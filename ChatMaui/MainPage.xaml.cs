using Microsoft.AspNetCore.SignalR.Client;
using Models;

namespace ChatMaui
{
    public partial class MainPage : ContentPage
    {
        private readonly HubConnection _connection;

        public MainPage()
        {
            InitializeComponent();

            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5279/chathub")
                .Build();

            _connection.On<MensajeUsuario>("ReceiveMessage", (mensajeUsuario) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    chatMessages.Text += $"{mensajeUsuario.Usuario}: {mensajeUsuario.Mensaje} \n";
                });
            });

            Task.Run(() => 
            {
                Dispatcher.Dispatch(async () => await _connection.StartAsync());
            });
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            MensajeUsuario mensajeUsuario = new MensajeUsuario();
            mensajeUsuario.Usuario = myUsername.Text;
            mensajeUsuario.Mensaje = myChatMessages.Text;

            await _connection.InvokeCoreAsync("SendMessage", args: new[]
                { mensajeUsuario});

            myChatMessages.Text = String.Empty;
        }
    }

}
