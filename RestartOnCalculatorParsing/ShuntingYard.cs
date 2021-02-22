using System;
using System.Collections.Generic;
using System.Text;

namespace RestartOnCalculatorParsing
{
    public class Token
    {
        public char Value { get; set; }
        public Token(char v)
        {
            Value = v;
        }
    }

    public static class ShuntingYard
    {
        public static Dictionary<char, int> OperatorPrecedence = new Dictionary<char, int>()
        {
            {'+', 0},
            {'-', 0},
            {'/', 1},
            {'*', 1},
            {'^', 2}
        };
        public static Queue<char> Parse(string text)
        {
            List<Token> tokens = new List<Token>();
            for (int i = 0; i < text.Length; i++)
            {
                if(text[i] != ' ')
                {
                    tokens.Add(new Token(text[i]));
                }
            }
            Queue<char> output = new Queue<char>();
            Stack<char> operatorStack = new Stack<char>();
            text.Trim();
            for (int pos = 0; pos < tokens.Count; pos++)
            {
                if (char.IsDigit(tokens[pos].Value) || char.IsLetter(tokens[pos].Value))
                {
                    output.Enqueue(tokens[pos].Value);
                }
                else if(tokens[pos].Value == '(')
                {
                    operatorStack.Push(tokens[pos].Value);
                }
                else if(tokens[pos].Value == ')')
                {
                    while(operatorStack.Peek() != '(')
                    {
                        output.Enqueue(operatorStack.Pop());
                    }

                }
                else
                {
                    while(operatorStack.Count > 0 && operatorStack.Peek() != '(' && OperatorPrecedence[operatorStack.Peek()] >= OperatorPrecedence[tokens[pos].Value])
                    {
                        output.Enqueue(operatorStack.Pop());
                    }
                    operatorStack.Push(tokens[pos].Value);
                }
            }
            while(operatorStack.Count > 0)
            {
                output.Enqueue(operatorStack.Pop());
            }
            return output;
        }
    }
}
