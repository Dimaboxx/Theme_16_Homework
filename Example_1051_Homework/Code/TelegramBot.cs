using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Windows.Threading;
using Telegram.Bot.Types;
using File = System.IO.File;
using Telegram.Bot.Types.InputFiles;

namespace Example_1051_Homework_10
{
    class TelegramHelper
    {
        public Action<tgMessage> newMessage;
        public TelegramBotClient tgbotClient { get; set; }
        private MainWindow w;
        private string comandlist;
        public TelegramHelper(MainWindow window)
        {
            w = window;
            #region Create access

            string token = File.ReadAllText(@"Z:\_Education\C#\C# с нуля до PRO\Theme_09_T.me bot settings\Token.txt");
            #region Proxy

            //  Реализация прокси уехала на будующее, так как поиск рабочего прокси занимал очень много времени.
            #region liks
            //// https://hidemyna.me/ru/proxy-list/?maxtime=250#list
            #endregion
            //var proxy = new WebProxy()
            //{
            //    Address = new Uri($"http://51.158.114.177:8811"),
            //    UseDefaultCredentials = false,
            //    //Credentials = new NetworkCredential(userName: "login", password: "password")
            //};
            //// Создает экземпляр класса System.Net.Http.HttpClientHandler.
            //var httpClientHandler = new HttpClientHandler() { Proxy = proxy };

            //// Предоставляет базовый класс для отправки HTTP-запросов и получения HTTP-ответов 
            //// от ресурса с заданным URI.
            ///
            #endregion
            HttpClient hc = new HttpClient();
            #endregion

            //string test = $"http://api.telegram.org/bot{token}/getMe";

            //var t = hc.GetStringAsync(test);
            //var s = t.Result;
            comandlist = "/start - приветсвие\n" +
                "/help - список команд\n" +
                "/hw <улица>- вывод данных по отключению горячей воды для <улица> \n";



            tgbotClient = new TelegramBotClient(token, hc);
            getme();
            tgbotClient.OnMessage += MessageListenerAsync;
            tgbotClient.StartReceiving();
        }

        public async void getme()
        {
            Telegram.Bot.Types.User me = await tgbotClient.GetMeAsync();
            Global.ME.ID = me.Id;
            Global.ME.Username = me.Username;
            Global.ME.FirstName = me.FirstName;
        }


        private async void MessageListenerAsync(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            tgMessage tgMessage = await tgMessagefromMessageAsync(e.Message);
            w.Dispatcher.Invoke(() =>
           {
               newMessage?.Invoke(tgMessage);
           });
            switch (e.Message.Type)
            {
                case Telegram.Bot.Types.Enums.MessageType.Text: getTextMeesageAsync(e.Message); break;
                case Telegram.Bot.Types.Enums.MessageType.Sticker: AnswerStickerAsync(e.Message);  break;
                //case Telegram.Bot.Types.Enums.MessageType.Text: break;
                default: break;
            }
        }


        private async void getTextMeesageAsync(Telegram.Bot.Types.Message message)
        {
            if (message.Text.StartsWith("/start"))
            {
                AnswerTextAsync(message.Chat, $"hello {message.From.Username}! \n {comandlist}");
            }
            else if (message.Text.StartsWith("/hw"))
            {
                string a = await OpenData_Mos_ru.OpenData.GetdataStringAsync(15,  message.Text.Substring(4) );
                AnswerTextAsync(message.Chat, a);
            }
            else if (message.Text.StartsWith("/help"))
            {
                AnswerTextAsync(message.Chat, $"{comandlist}");
            }
        }

        private async void AnswerTextAsync(Chat chat,  string text)
        {
            Message answer = await tgbotClient.SendTextMessageAsync(chat.Id, text);
            tgMessage tgMessage = await tgMessagefromMessageAsync(answer);
            w.Dispatcher.Invoke(() =>
            {
                newMessage?.Invoke(tgMessage);
            });
        }

        private async void AnswerStickerAsync(Message message)
        {
            Message answer = await tgbotClient.SendStickerAsync(message.Chat.Id, message.Sticker.FileId);
            tgMessage tgMessage = await tgMessagefromMessageAsync(answer);
            w.Dispatcher.Invoke(() =>
            {
                newMessage?.Invoke(tgMessage);
            });
        }

        private async Task<tgMessage> tgMessagefromMessageAsync( Message message)
        {   switch (message.Type)
            {
                case Telegram.Bot.Types.Enums.MessageType.Text:
                    return new tgMessage(message.Chat.Id, message.From, message.MessageId, message.Type.totgMessageType(), message.Text, message.Date);
                case Telegram.Bot.Types.Enums.MessageType.Photo:
                    {
                        var fileid = message.Photo[message.Photo.Length - 1].FileId; //берем самое большое фото
                        var file = await tgbotClient.GetFileAsync(fileid);
                        string filename = message.MessageId.ToString() + "_photo.jpg";
                        FileStream fs = new FileStream(filename, FileMode.Create);
                        await tgbotClient.DownloadFileAsync(file.FilePath, fs);
                        fs.Close();
                        fs.Dispose();
                        return new tgMessage(message.Chat.Id, message.From, message.MessageId, tgMessageType.foto, System.IO.Path.GetFullPath(filename), message.Date);
                    }
                case Telegram.Bot.Types.Enums.MessageType.Sticker:
                    {
                        var fileid = message.Sticker.FileId;
                        var file = await tgbotClient.GetFileAsync(fileid);
                        string filename = message.MessageId.ToString() + "_stiker.jpg";
                        FileStream fs = new FileStream(filename, FileMode.Create);
                        await tgbotClient.DownloadFileAsync(file.FilePath, fs);
                        fs.Close();
                        fs.Dispose();
                        return new tgMessage(message.Chat.Id, message.From, message.MessageId, tgMessageType.foto, System.IO.Path.GetFullPath(filename), message.Date);
                    }
                default: return new tgMessage(message.Chat.Id, message.From, message.MessageId, tgMessageType.text, $"получено сообщение типа {message.Type} \n данный тип не обрабатывается ", message.Date) ;
            }
        }
        //public static tgMessage totgMessage(this Telegram.Bot.Types.Message message)
        //{
        //    return new tgMessage(message.Chat.Id, message.From, message.MessageId, message.Type.totgMessageType(), message.Text, message.Date);
        //}
    }


}
#region old code

//        public async Task GetMessagesAsync()
//        {
//            int offset = 0;
//            do
//            {
//                Telegram.Bot.Types.Update[] upd = await botClient.GetUpdatesAsync(offset);
//                foreach (var u in upd)
//                {
//                    Console.WriteLine("{0} от {1}", u.Message.Type, u.Message.From.Username);
//                    string fileid;
//                    //перебераем некотрые типы сообщений
//                    switch (u.Message.Type)
//                    {


//                        case Telegram.Bot.Types.Enums.MessageType.Sticker:
//                            {
//                                fileid = u.Message.Sticker.FileId;
//                                var file = await botClient.GetFileAsync(fileid);
//                                string filename = u.Message.MessageId.ToString() + "_stiker.jpg";
//                                FileStream fs = new FileStream(filename, FileMode.Create);
//                                await botClient.DownloadFileAsync(file.FilePath, fs);
//                                fs.Close();
//                                fs.Dispose();
//                                //Process.Start(filename);
//                                mesrep.Addmessage(new Message(
//                                                        u.Message.From.Username,
//                                                        u.Message.MessageId,
//                                                        MessageType.foto,
//                                                        filename,
//                                                        u.Message.Date), u.Message.From.Id);

//                                break;
//                            }
//                        case Telegram.Bot.Types.Enums.MessageType.Document:
//                            {
//                                fileid = u.Message.Document.FileId;
//                                var file = await botClient.GetFileAsync(fileid);
//                                string filename = u.Message.MessageId.ToString() + "_" + u.Message.Document.FileName;
//                                FileStream fs = new FileStream(filename, FileMode.Create);
//                                await botClient.DownloadFileAsync(file.FilePath, fs);
//                                fs.Close();
//                                fs.Dispose();
//                                mesrep.Addmessage(new Message(
//                                                    u.Message.From.Username,
//                                                    u.Message.MessageId,
//                                                    MessageType.file,
//                                                    filename,
//                                                    u.Message.Date), u.Message.From.Id);
//                                //Process.Start(filename);
//                                break;
//                            }
//                    }

//                    if (mesrep.chats.Count > 0)
//                    {
//                        w.TelegramlogList.ItemsSource = mesrep.chats.Find(x => x.UserID == u.Message.From.Id).messages;
//                        w.userName.Text = mesrep.chats.Find(x => x.UserID == u.Message.From.Id).UserID.ToString();
//                }
//                    Telegram.Bot.Types.Message b = await botClient.SendTextMessageAsync(u.Message.Chat.Id, "Для получения данных по отключению горячей воды введите адрес\n" +
//                        "например Бауманская улица, дом 21");
//                    mesrep.Addmessage(new Message(
//                             b.From.Username,
//                             b.MessageId,
//                             MessageType.text,
//                             b.Text,
//                             b.Date),
//                            u.Message.From.Id);

//                    //SendPhotoAsync(u.Message.Chat.Id,    );
//                    offset = ++u.Id;
//                }
//               Thread.Sleep(1000);
//            } while (true);
//        }

#endregion