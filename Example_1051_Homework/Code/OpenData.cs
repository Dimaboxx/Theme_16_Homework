using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Example_1051_Homework_10
{
    class OpenData
    {
        static WebClient webClient = new WebClient();
        static string token = File.ReadAllText(@"C:\_Education\C#\C# с нуля до PRO\Theme_09_mos.ru\token.txt");
        /// <summary>
        /// подготовка
        /// </summary>
        static OpenData()
        {
            webClient.Encoding = System.Text.Encoding.UTF8;
             webClient.BaseAddress = @"https://apidata.mos.ru/v" + GetVersion() + "/datasets/1401/rows/"; ///
        }
        /// <summary>
        /// получение данных с портала данных по отключению горячей воды mos.ru
        /// </summary>
        /// <param name="n"> число адресов при совпадении адреса с несолькими объектами</param>
        /// <param name="adress"> адрес или часть адреса</param>
        /// <returns></returns>
        static public  string Getdata(int n, string adress)
        {
            string url = "?" + "$top=" + n.ToString() + "&" +
                                "$filter=Cells/Address eq " + adress + "&" +
                                "api_key=" + token;
            string webresult = webClient.DownloadString(url);
            var jArray = JArray.Parse(webresult);
           if (jArray.Count == 0)
                return "адрес не найден";
             string result = "";
            foreach( var jr in jArray)
            {
                result += jr["Cells"]["Address"].ToString() + " : " + jr["Cells"]["Periods"][0]["OutageBegin"].ToString() + "..." + jr["Cells"]["Periods"][0]["OutageEnd"].ToString() + "\n";
            }
            return result;
        }

        static public List<datamos> Getdata(string n , string adress)
        {
            string url = "?" + "$top=" + n + "&" +
                                "$filter=Cells/Address eq " + adress + "&" +
                                "api_key=" + token;
            string webresult = webClient.DownloadString(url);
            var jArray = JArray.Parse(webresult);
            List<datamos> result = new List<datamos>();
            if (jArray.Count == 0)
                return result;
            foreach (var jr in jArray)
            {
                result.Add(new datamos()
                {
                    Address = jr["Cells"]["Address"].ToString(),
                    OutageBegin = jr["Cells"]["Periods"][0]["OutageBegin"].ToString(),
                    OutageEnd = jr["Cells"]["Periods"][0]["OutageEnd"].ToString()
                });
                 
            }
            return result;
        }
        /// <summary>
        /// получаем версию схемы данных портала mos.ru
        /// </summary>
        /// <returns></returns>
        static string GetVersion()
        {
            string result = webClient.DownloadString(@"https://apidata.mos.ru/version");
            return JObject.Parse(result)["Version"].ToString();
        }

    }
}
