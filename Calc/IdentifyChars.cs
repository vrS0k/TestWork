using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverse_Polish_Notation
{
    class IdentifyChars
    {

        public static bool IsDelimeter(char c)
        {
            if (" ".IndexOf(c) != -1)
                return true;
            return false;
        }

        public static bool IsOperator(char с)
        {
            if ("+-/*^()".IndexOf(с) != -1)
                return true;
            return false;
        }

        public static bool IsUnarOperator(char a)
        {
            if ("sctg~".IndexOf(a) != -1)
                return true;
            return false;
        }
    }
}
