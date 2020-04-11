using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;


namespace Example_1051_Homework_10
{
    /// <summary>
    /// Репозитарий чатов(сообщений) 
    /// сообщения хранятся в чатах, чаты содержат id user блок сообщений.
    /// </summary>
    class tgMessageRepository
    {
        public ObservableCollection<tgChat> chats;

        /// <summary>
        /// def construcor
        /// </summary>
        public tgMessageRepository()
        {
            chats = new ObservableCollection<tgChat>();
        }

        public void Addmessage(tgMessage msg)
        {
            if (!chats.Contains(new tgChat(msg.ChatId)))
            {
                chats.Add(new tgChat (msg.ChatId,msg.From));
            }
            chats[chats.IndexOf(new tgChat(msg.ChatId))].messages.Add(msg);
        }
    }




}
