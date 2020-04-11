using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_1051_Homework_10
{
    public class tgUser : IEquatable<tgUser>, INotifyPropertyChanged
    {
        private int id;
        private string firstName;
        private string username;

        public int ID
        {
            get { return id; }

            set { id = value; }
        }
        public string FirstName 
        { 
            get => firstName;
            set { 
                firstName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof (FirstName)));
            } 
        }
        public string Username 
        { 
            get => username;
            set
            {
                username = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Username)));
            }
        }


        /// <summary>
        /// для интерфейса
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public tgUser()
        {

        }

        public tgUser(int id, string firstname, string username)
        {
            ID = id;
            FirstName = firstname;
            Username = username;
        }


        public bool Equals(tgUser other)
        {
            return (this.ID == other.ID);
        }

        public static implicit operator tgUser(Telegram.Bot.Types.User user)
        {
            return new tgUser()
            {
                ID = user.Id,
                FirstName = user.FirstName,
                Username = user.Username
            }

            ;
        }

    }
}
