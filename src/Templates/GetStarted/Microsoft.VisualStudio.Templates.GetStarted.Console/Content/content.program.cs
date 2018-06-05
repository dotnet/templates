using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
$if$ ($targetframeworkversion$ >= 4.5)using System.Threading.Tasks;
$endif$
namespace $safeprojectname$
{
    class Program
    {
        static void Main(string[] args)
        {
            // Press F5 on the keyboard to debug this app
            // Go here for more information:
            // https://aka.ms/console-app-get-started

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
