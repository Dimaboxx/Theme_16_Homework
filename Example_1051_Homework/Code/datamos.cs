using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_1051_Homework_10
{
    class datamos
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
