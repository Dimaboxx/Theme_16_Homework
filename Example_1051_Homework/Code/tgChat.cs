using System;
using System.Collections.ObjectModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_1051_Homework_10
{
    public class tgChat : IEquatable<tgChat>
    {
        /// <summary>
        /// Коллекция сообщений с данным пользовааателем
        /// </summary>
        public ObservableCollection<tgMessage> messages { get; }
        public tgChat( long ChatId)
        {
            this.ID = ChatId;
            messages = new ObservableCollection<tgMessage>();
        }



        public tgChat(long ChatId, tgUser user) : this(ChatId)
        {
            User = user;
        }


        public tgUser User { get; set; }
        public long ID { get; set; }

        public bool Equals(tgChat other)
        {
            if (other == null) return false;
            return (this.ID.Equals(other.ID));
        }
    }

}
