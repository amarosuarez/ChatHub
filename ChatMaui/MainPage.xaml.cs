using Microsoft.AspNetCore.SignalR.Client;
using Models;

namespace ChatMaui
{
    public partial class MainPage : ContentPage
    {
        private readonly HubConnection _connection;
        private String _room;

        public MainPage()
        {
            InitializeComponent();

            //_connection = new HubConnectionBuilder()
            //    .WithUrl("http://localhost:5279/chathub")
            //    .Build();

            ////_connection.On<MensajeUsuario>("ReceiveMessage", (mensajeUsuario) =>
            ////{
            ////    MainThread.BeginInvokeOnMainThread(() =>
            ////    {
            ////        chatMessages.Text += $"{mensajeUsuario.Usuario}: {mensajeUsuario.Mensaje} \n";
            ////    });
            ////});

            //_connection.On<MensajeUsuario>("ReceiveMessage", ReceiveMessage);

            //Task.Run(() => 
            //{
            //    Dispatcher.Dispatch(async () => await _connection.StartAsync());
            //});
        }

        //private async void ReceiveMessage(MensajeUsuario mensajeUsuario)
        //{
        //    Dispatcher.Dispatch(async () => chatMessages.Text += $"{mensajeUsuario.Usuario}: {mensajeUsuario.Mensaje} \n");
        //}

        //private async void OnCounterClicked(object sender, EventArgs e)
        //{   
        //    if (!string.IsNullOrEmpty(_room))
        //    {
        //        MensajeUsuario mensajeUsuario = new MensajeUsuario();
        //        mensajeUsuario.Sala = groupName.Text;
        //        mensajeUsuario.Usuario = myUsername.Text;
        //        mensajeUsuario.Mensaje = myChatMessages.Text;

        //        await _connection.InvokeCoreAsync("SendMessage", args: new[]
        //            { mensajeUsuario});

        //        myChatMessages.Text = String.Empty;
        //    }
        //    else
        //    {
        //        error.Text = "Debes unirte a un grupo";
        //        error.IsVisible = true;
        //    }
        //}

        //private async void joinGroupButton_Clicked(object sender, EventArgs e)
        //{
        //    String group = groupName.Text;

        //    // Comprueba que haya escrito un grupo
        //    if (!string.IsNullOrEmpty(group))
        //    {
        //        // Comprueba si ya estaba en un grupo y que no sea el mismo
        //        if (!string.IsNullOrEmpty(_room) && !_room.Equals(group))
        //        {
        //            // Sale del grupo anterior
        //            await _connection.InvokeCoreAsync("LeaveRoom", args: new[]
        //                { _room });

        //            // Unimos al nuevo grupo
        //            addGroup(group);
        //        } else
        //        {
        //            // Unimos al nuevo grupo
        //            addGroup(group);
        //        }
        //    } else
        //    {
        //        error.Text = "Escribe el nombre de un grupo";
        //        error.IsVisible = true;
        //    }

        //}

        //private async void addGroup(String group)
        //{
        //    // Limpiamos
        //    _room = "";

        //    // Se une al grupo nuevo
        //    await _connection.InvokeCoreAsync("JoinRoom", args: new[]
        //        { group });

        //    _room = group;
        //    error.IsVisible = false;
        //    chatMessages.Text = "";
        //}

        //private async void removeGroupButton_Clicked(object sender, EventArgs e)
        //{
        //    String group = groupName.Text;

        //    await _connection.InvokeCoreAsync("LeaveRoom", args: new[]
        //        { _room });

        //    error.IsVisible = true;
        //    error.Text = "Has salido del grupo " + _room;
        //    _room = "";
        //    groupName.Text = "";
        //    chatMessages.Text = "";
        //}
    }

}
