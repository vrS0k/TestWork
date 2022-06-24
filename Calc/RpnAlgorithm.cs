using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static Reverse_Polish_Notation.IdentifyChars;

namespace Reverse_Polish_Notation
{
    class RpnAlgorithm
    {

        static private byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': case '/': return 4;
                case '~': return 5;
                case '^': return 6;
               
                default: return 7;
            }
        }

        static private double CalculationsIfBinaryOperator(char input, double a, double b)
        {

            double result = 0;

            switch (input)
            {
                case '+': result = b + a; break;
                case '-': result = b - a; break;
                case '*': result = b * a; break;
                case '/':
                    try
                    {
                        result = b / a;
                        if (a == 0)
                            throw new DivideByZeroException();
                    }
                    catch (DivideByZeroException)
                    {
                        MessageBox.Show("Деление на ноль!");
                        result = b / a;
                    }
                    break;

                case '^': result = Math.Pow(b, a); break;
            }
            return result;
        }

        static private double CalculationIfUnarOperator(char input, double a)
        {
            double result = 0;

            switch (input)
            {
                case '~': result = -a; break;
                case 's': result = Math.Sin(a); break;
                case 'c': result = Math.Cos(a); break;
                case 't': result = Math.Tan(a); break;
                case 'g':
                    try
                    {
                        result = 1 / Math.Tan(a);
                    }
                    catch (DivideByZeroException)
                    {
                        MessageBox.Show("Деление на ноль!");
                        result = 1 / Math.Tan(a);
                    }
                    break;
            }
            return result;
        }
        
        static public ResultOfCounting Calculate(string input)
        {
            input = input.Replace('.', ',');

            string output = GetExpression(input);
            
            ResultOfCounting result = Counting(output); 
            return result; 

        }

        static public string GetExpression(string input)
        {
            string output = string.Empty; 
            Stack<char> operStack = new Stack<char>();
            
            for (int i = 0; i < input.Length; i++)
            {

                if (IsDelimeter(input[i]))
                    continue;

                if (input[i] == '-')
                    if (i == 0 || (i > 0 && "+-*^/(".IndexOf(input[i - 1]) != -1))
                    {
                        operStack.Push('~');
                        continue;
                    }

                if (Char.IsDigit(input[i]))
                {

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]) && !IsUnarOperator(input[i]))
                    {
                        output += input[i];
                        i++;
                        if (i == input.Length) break;
                    }

                    output += " ";
                    i--; 

                }
                else if (IsOperator(input[i]) || IsUnarOperator(input[i]))
                {
                    if (operStack.Count > 0 && input[i] != '(')
                    {
                        if (input[i] == ')')
                        {
                            char c = operStack.Pop();
                            while (c != '(')
                            {
                                output += c + " ";
                                c = operStack.Pop();
                            }
                        }
                        else if (GetPriority(input[i]) > GetPriority(operStack.Peek()))
                            operStack.Push(char.Parse(input[i].ToString()));
                        else
                        {
                            while (operStack.Count > 0 && GetPriority(input[i]) <= GetPriority(operStack.Peek()))
                                output += operStack.Pop().ToString() + " ";
                            operStack.Push(char.Parse(input[i].ToString()));
                        }
                    }
                    else
                        operStack.Push(char.Parse(input[i].ToString()));
                }                
            }
            while (operStack.Count > 0)
            output += operStack.Pop() + " ";
            return (output);
        }

        static private ResultOfCounting Counting(string input)
        {
            double result = 0; 
            Stack<double> temp = new Stack<double>(); 
            string number;
            for (int i = 0; i < input.Length; i++) 
            {
                number = string.Empty;
                if (char.IsDigit(input[i]))
                {
                    
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]) && !IsUnarOperator(input[i]))
                    {
                        number += input[i];
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(double.Parse(number)); 
                    i--;
                }
                else if (IsOperator(input[i]) )
                {
                    double a, b;

                    try
                    {
                        a = temp.Pop();
                        b = temp.Pop();
                        result = CalculationsIfBinaryOperator(input[i], a, b);
                        temp.Push(result);
                    }

                    catch (InvalidOperationException)
                    {
                        return new ResultOfCounting(0, false, string.Empty);
                    }
                }

                else if (IsUnarOperator(input[i])) 
                {                  
                    double a;
                    try
                    {
                        a = temp.Pop();
                        result = CalculationIfUnarOperator(input[i], a);
                        temp.Push(result);                      
                    }
                    catch (InvalidOperationException)
                    {
                        return new ResultOfCounting(0, false, string.Empty);
                    }
                }
            }
            return new ResultOfCounting(temp.Peek(), true, input); 
        }
    }

    public struct ResultOfCounting
    {
        public readonly double Value;
        public readonly bool IsCorrectInput;
        public readonly string Expression;
        public ResultOfCounting(double val, bool flag, string exp)
        {
            Value = val;
            IsCorrectInput = flag;
            Expression = exp;
        }
    }
}