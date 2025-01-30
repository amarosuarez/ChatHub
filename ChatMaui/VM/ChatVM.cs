using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using ViewModels.Utilidades;

namespace ChatMaui.VM
{
    public class ChatVM : INotifyPropertyChanged
    {
        #region Atributos
        private readonly HubConnection _connection;
        private String _room;
        private String group;
        private String name;
        private String message;
        private String chatMessages;
        private DelegateCommand joinGroupCommand;
        private DelegateCommand exitGroupCommand;
        private DelegateCommand sendMessageCommand;
        #endregion

        #region Propiedades
        public String Group
        {
            get { return group; }
            set { group = value; }
        }

        public String Name
        {
            get { return name; }
            set { 
                name = value;
                sendMessageCommand.RaiseCanExecuteChanged();
            } 
        }

        public String Message
        {
            get { return message; }
            set { 
                message = value;
                sendMessageCommand.RaiseCanExecuteChanged();
            }
        }

        public String ChatMessages
        {
            get { return chatMessages; }
            set { chatMessages = value; }
        }

        public DelegateCommand JoinGroupCommand
        {
            get { return joinGroupCommand; }
        }

        public DelegateCommand ExitGroupCommand
        {
            get { return exitGroupCommand; }
        }

        public DelegateCommand SendMessageCommand
        {
            get { return sendMessageCommand; }
        }
        #endregion

        #region Constructores
        public ChatVM()
        {
            // Commands
            joinGroupCommand = new DelegateCommand(joinGroupCommandExecuted, joinGroupCommandCanExecute);
            exitGroupCommand = new DelegateCommand(exitGroupCommandExecuted, exitGroupCommandCanExecute);
            sendMessageCommand = new DelegateCommand(sendMessageCommandExecuted, sendMessageCommandCanExecute);

            // Creamos la conexión
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5279/chathub")
                .Build();

            // Nos conectamos
            Task.Run(async () =>
            {
                await MainThread.InvokeOnMainThreadAsync(async () => await _connection.StartAsync());
            });

            // Recibo los mensajes de forma automática
            _connection.On<MensajeUsuario>("ReceiveMessage", receiveMessage);
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método que recibe los mensajes automáticamente y los pinta en la pantalla<br>
        /// Pre: Ninguno</br>
        /// Post: Ninguno
        /// </summary>
        /// <param name="mensajeUsuario">Nuevo mensaje a pintar</param>
        private async void receiveMessage(MensajeUsuario mensajeUsuario)
        {
            await MainThread.InvokeOnMainThreadAsync(async () => chatMessages  += $"{mensajeUsuario.Usuario}: {mensajeUsuario.Mensaje}\n");
            NotifyPropertyChanged(nameof(ChatMessages));
        }
        #endregion

        #region Commands
        /// <summary>
        /// Función que une al usuario al grupo especificado <br>
        /// Pre: Debe haberse especificado el grupo</br>
        /// Post: Ninguno
        /// </summary>
        public async void joinGroupCommandExecuted()
        {
            if (!string.IsNullOrEmpty(group))
            {
                await _connection.InvokeCoreAsync("JoinRoom", args: new[]
                 { group });

                _room = group;

                sendMessageCommand.RaiseCanExecuteChanged();
                joinGroupCommand.RaiseCanExecuteChanged();
                exitGroupCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Función que determina si el command para unir al usuario al grupo está activo o no<br>
        /// Pre: Ninguno</br>
        /// Post: Ninguno
        /// </summary>
        /// <returns>Puede ejecutarse o no el command</returns>
        public bool joinGroupCommandCanExecute()
        {
            return string.IsNullOrEmpty(_room);
        }

        /// <summary>
        /// Función que saca al usuario del grupo en el que se encuentra<br>
        /// Pre: Ninguno</br>
        /// Post: Ninguno
        /// </summary>
        public async void exitGroupCommandExecuted()
        {
            await _connection.InvokeCoreAsync("LeaveRoom", args: new[]
                { _room });

            chatMessages = "";
            group = "";
            _room = group;
            NotifyPropertyChanged(nameof(ChatMessages));
            NotifyPropertyChanged(nameof(Group));

            joinGroupCommand.RaiseCanExecuteChanged();
            exitGroupCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Función que determina si el command para sacar al usuario del grupo está activo o no<br>
        /// Pre: Ninguno</br>
        /// Post: Ninguno
        /// </summary>
        /// <returns>Puede ejecutarse o no el command</returns>
        public bool exitGroupCommandCanExecute()
        {
           return !string.IsNullOrEmpty(_room);
        }
        
        /// <summary>
        /// Función que envía el mensaje<br>
        /// Pre: Debe haberse escrito el nombre y el mensaje</br>
        /// Post: Ninguno
        /// </summary>
        public async void sendMessageCommandExecuted()
        {
            MensajeUsuario mensajeUsuario = new MensajeUsuario();
            mensajeUsuario.Sala = group;
            mensajeUsuario.Usuario = name;
            mensajeUsuario.Mensaje = message;

            await _connection.InvokeCoreAsync("SendMessage", args: new[]
                { mensajeUsuario});

            message = String.Empty;
            NotifyPropertyChanged(nameof(Message));
        }

        /// <summary>
        /// Función que determina si el command para enviar el mensaje está activo o no <br>
        /// Pre: Ninguno</br>
        /// Post: Ninguno
        /// </summary>
        /// <returns>Puede ejecutarse o noel command</returns>
        public bool sendMessageCommandCanExecute()
        {
            return !string.IsNullOrEmpty(_room) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(message);
        }
        #endregion

        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")

        {

            PropertyChanged?.Invoke(this, new
            PropertyChangedEventArgs(propertyName));

        }
        #endregion
    }
}
