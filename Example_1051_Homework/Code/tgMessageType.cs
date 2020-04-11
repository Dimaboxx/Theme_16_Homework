using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_1051_Homework_10
{
    public enum tgMessageType
    {
        text,
        foto,
        file,
        other
    }

    public static class ext
    {
        public static tgMessageType totgMessageType(this Telegram.Bot.Types.Enums.MessageType messageType)
        {
            switch (messageType)
            {
                case Telegram.Bot.Types.Enums.MessageType.Text: return tgMessageType.text;
                case Telegram.Bot.Types.Enums.MessageType.Photo: return tgMessageType.foto;
                case Telegram.Bot.Types.Enums.MessageType.Sticker: return tgMessageType.foto;
                default: return tgMessageType.other;
            }
        }
    }
}
