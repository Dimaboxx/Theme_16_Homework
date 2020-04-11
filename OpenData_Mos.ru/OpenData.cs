using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace OpenData_Mos_ru
{
    public class OpenData
    {
        static WebClient webClient;
        public static string token;
        static string OpenDateVersions;
        /// <summary>
        /// подготовка
        /// </summary>
        static OpenData()
        {
            webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            OpenDateVersions = GetVersion();
            webClient.BaseAddress = @"https://apidata.mos.ru/v" + OpenDateVersions + "/datasets/1401/rows/"; ///
        }


        /// <summary>
        /// Получение номера текущей схемы данных портала OPENdata.mos.ru
        /// </summary>
        /// <returns></returns>
        public static string GetVersion()
        {
            string result = webClient.DownloadString(@"https://apidata.mos.ru/version");
            return JObject.Parse(result)["Version"].ToString();
        }


        /// <summary>
        /// Получение async ответа с портала открытых данных
        /// </summary>
        /// <param name="reguest"></param>
        /// <returns></returns>
        static public async Task<string> GetOpendataStringAsync(ReguestItem reguest)
        {
            string webresult = await webClient.DownloadStringTaskAsync(reguest.Request);
            return webresult;
        }

        /// <summary>
        /// Получение ответа синхронно 
        /// </summary>
        /// <param name="reguest"></param>
        /// <returns></returns>
        static public string GetOpendataString(ReguestItem reguest)
        {
            string webresult =  webClient.DownloadString(reguest.Request);
            return webresult;
        }




        /// <summary>
        /// получение данных с портала данных по отключению горячей воды mos.ru в виде строки
        /// </summary>
        /// <param name="n"> число адресов при совпадении адреса с несолькими объектами</param>
        /// <param name="adress"> адрес или часть адреса</param>
        /// <returns></returns>
        static public async Task<string> GetdataStringAsync(int n, string adress)
        {
            ReguestItem req = new ReguestItem()
            {
                TopNumber = n,
                SkipNumber = 0,
                Filter = "Cells/Address eq " + adress
            };
            var webreq = await GetOpendataStringAsync(req);
            string result = HotWaterOutegeJsonParseToString(webreq);
            return result;

        }
        static public string GetdataString(int n, string adress)
        {
            ReguestItem req = new ReguestItem()
            {
                TopNumber = n,
                SkipNumber = 0,
                Filter = "Cells/Address eq " + adress
            };
            var webreq = GetOpendataString(req);
            string result = HotWaterOutegeJsonParseToString(webreq);
            return result;

        }
        


        static public async Task<List<datamos>> GetdataListAsync(int n , string adress)
        {
            ReguestItem req = new ReguestItem()
            {
                TopNumber = n,
                SkipNumber = 0,
                Filter = "Cells/Address eq " + adress
            };
            var webreq = await GetOpendataStringAsync(req);
            var result = HotWaterOutegeJsonParseToList(webreq);
            return result;
        }

        /// <summary>
        /// получаем версию схемы данных портала mos.ru
        /// </summary>
        /// <returns></returns>

         ///преобразование ответа в лист объектов
        static List<datamos> HotWaterOutegeJsonParseToList(string Json)
        {
            var jArray = JArray.Parse(Json);
            List<datamos> result = new List<datamos>();
            if (jArray.Count == 0)
                return result;
            File.WriteAllText("reusult.Json", Json);

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
        /// преобразованиеответа в строку
        /// </summary>
        /// <param name="Json"></param>
        /// <returns></returns>
        static string HotWaterOutegeJsonParseToString(string Json)
        {
            var jArray = JArray.Parse(Json);
            if (jArray.Count == 0)
                return "адрес не найден";
            string result = "";
            foreach (var jr in jArray)
            {
                result += jr["Cells"]["Address"].ToString() + " : " + jr["Cells"]["Periods"][0]["OutageBegin"].ToString() + "..." + jr["Cells"]["Periods"][0]["OutageEnd"].ToString() + "\n";
            }
            return result;
        }


    }
}
