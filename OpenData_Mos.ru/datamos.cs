using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenData_Mos_ru
{
    public class datamos
    {
        public string Address { get; set; }
        public string OutageBegin { get; set; }
        public string OutageEnd { get; set; }

        public override string ToString()
        {
            return $" Адрес : {Address} \n " +
                $"c {OutageBegin} по {OutageEnd}";
        }
    }
}
