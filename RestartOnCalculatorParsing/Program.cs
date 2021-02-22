using System;

namespace RestartOnCalculatorParsing
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string test = "3 + 4 * 2 / ( 1 − 5 )";
            var q = ShuntingYard.Parse(test);
            ;
        }
    }
}
