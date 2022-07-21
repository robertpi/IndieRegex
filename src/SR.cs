using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.RegularExpressions
{
    internal partial class SR
    {
        public static string Format(string x, object y) 
        {
            return string.Format(x, y);
        }


        public static string Format(string x, object y, object z)
        {
            return string.Format(x, y, z);
        }

        public static string Format(string x, object y, object z, object a)
        {
            return string.Format(x, y, z, a);
        }
    }
}
