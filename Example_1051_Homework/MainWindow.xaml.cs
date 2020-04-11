using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenData_Mos_ru;
using Example_1219Wpf2;
//using DreamConvertions;


namespace Example_1051_Homework_10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string DateTimeformat;
        public static string Timeformat;
        public static string Dateformat;
        TelegramHelper tgclient;
        tgMessageRepository TgMessageRepository;
        public MainWindow()
        {
            InitializeComponent();
            TgMessageRepository = new tgMessageRepository();
            TelegramUserList.ItemsSource = TgMessageRepository.chats;
            ReguestItem.APIKEy = File.ReadAllText(@"Z:\_Education\C#\C# с нуля до PRO\Theme_09_mos.ru\token.txt");
            tgclient = new TelegramHelper(this);
            tgclient.newMessage += TgMessageRepository.Addmessage;
            txt_me.DataContext = Global.ME;
            txt_time.DataContext = new TimeProvider();
        }

        static MainWindow()
        {
            Timeformat = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.LongTimePattern;
            Dateformat = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern;
            DateTimeformat = $"{Dateformat} {Timeformat}";
        }


        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    Action action = getupdate;

        //    Dispatcher.BeginInvoke(action);
        //}

        //private async void getupdate()
        //{

        //    MoslogList.ItemsSource = await OpenData.GetdataAsync(10, dataadress.Text);
        //    MoslogList.Items.Refresh();
        //}


    }
}
