using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Reverse_Polish_Notation.IdentifyChars;

namespace Reverse_Polish_Notation
{
    class CheckInput
    {
        public static bool IsCorrectBracketsAndOperators(string s)
        {
            var stack = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {

                if (s[i] == '(') stack.Push(s[i]);
                else if (s[i] == ')')
                {
                    if (stack.Count == 0) return false;

                    if (stack.Pop() != '(') return false;
                }

                else if (IsOperator(s[i]) || IsDelimeter(s[i])) continue;

                else if (Char.IsDigit(s[i]) || s[i] == ',' || s[i] == '.') continue;

                else
                    return false;
            }
            return stack.Count == 0;
        }
    }
}
