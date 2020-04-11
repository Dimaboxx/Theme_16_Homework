using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_1051_Homework_10
{
    public class tgMessage
    {
        public tgUser From { get; set; }
        public int Id { get; set; }
        public tgMessageType MessageType { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public long ChatId { get; set; }
        public string Align
        {
            get 
            {
                return (From.Equals(Global.ME)) ? "Left" : "Right";
            }
        }

                 
        public string Background
        {
            get
            {
                return (From.Equals(Global.ME)) ?   "DarkSeaGreen" : "LightBlue";
            }
        }

        public string Margin
        {
            get
            {
                return (From.Equals(Global.ME)) ? "52,2,2,2" : "2,2,52,2";
            }
        }
        


        /// <summary>
        /// конструктор полный
        /// </summary>
        /// <param name="from"> имя пользователя</param>
        /// <param name="id"> id сообщения в чате</param>
        /// <param name="type"> message.type</param>
        /// <param name="text">текст</param>
        /// <param name="datetime">дата и время</param>
        public tgMessage(long chatid,tgUser from, int id, tgMessageType type, string text, DateTime datetime)
        {
            this.ChatId = chatid;
            this.From = from;
            this.Id = id;
            this.MessageType = type;
            this.Text = text;
            this.Date = datetime;
        }

        //public static implicit operator tgMessage(Telegram.Bot.Types.Message message)
        //{
        //    return new tgMessage(message.Chat.Id, message.From, message.MessageId, message.Type.totgMessageType(), message.Text, message.Date);
        //}
    }



}
