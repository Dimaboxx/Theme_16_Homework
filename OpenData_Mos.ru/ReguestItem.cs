using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenData_Mos_ru
{
    public class ReguestItem
    {
        public static string APIKEy;
        ///число записей для запроса
        public int TopNumber;
        ///число записей для пропуска 
        public int SkipNumber;
        ///Номер датасета
        public int DatasetNumber;
        /// Запрос числа всех строк в ответе
        ///допустимые значения true false
        public bool inlinecount;
        /// Название колонки для сортировки
        public string Orderby;
        /// имя для фильтра
        public string Filter;
        /// Показывать иностанные подписи
        /// true / false
        public string Foreign;

        private string topnumberforrequest { get { 
                return ((TopNumber > 0) ? $"$top= {TopNumber.ToString()}&" : String.Empty  );
        }}

        private string skipnumberforrequest { get {
                return ((SkipNumber > 0) ? $"$skip= {SkipNumber.ToString()}&" : String.Empty);
        }}

        private string inlinecountforrequest
        {
            get
            {
                return inlinecount ? "$inlinecount=allpages&" : String.Empty;
            }
        }
        private string apikeyforrequest
        {
            get
            {
                return $"api_key={APIKEy}";
            }
        }
        private string filterforrequest
        {
            get
            {//$filter=Cells/Address eq " + adress + "&"
                return (!(string.IsNullOrEmpty(Filter)) ? $"$filter={Filter}&" : String.Empty);
            }
        }



        public string Request
        {
            get
            {
                return "?"  +   topnumberforrequest +
                                skipnumberforrequest + 
                                filterforrequest + 
                                inlinecountforrequest + 
                                apikeyforrequest;
            }

        }
    }
}
