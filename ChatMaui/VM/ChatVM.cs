using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Utilidades;

namespace ChatMaui.VM
{
    public class ChatVM
    {
        #region Atributos
        private String room;
        private String group;
        private String name;
        private String message;
        private DelegateCommand joinGroupCommand;
        private DelegateCommand exitGroupCommand;
        private DelegateCommand sendMessageCommand;
        #endregion

        #region Propiedades
        public String Group
        {
            get { return room; }
            set { room = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; } 
        }

        public String Message
        {
            get { return message; }
            set { message = value; }
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

        }
        #endregion
    }
}
