using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsoleApp
{

    class A
    {
        public static int number;
        public static void Count()
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("In Class A " + i);
                number++;
            }
        }
    }

    class B
    {
        public static int number;
        public static void Count()
        {
            for (int i = 0; i < 1000; i++)
            {
                number++;
                if(number == 10)
                {
                    Console.WriteLine("______________________________________________________");
                }
                Console.WriteLine("In Class B "+i);
            }
        }
    }
    internal class ThreadExample
    {

    }
}
