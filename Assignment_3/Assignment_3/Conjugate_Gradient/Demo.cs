using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conjugate_Gradient
{
    class Demo
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine();
            Console.WriteLine("//////////////FR//////////////");
            Console.WriteLine();
            Console.WriteLine(FR.Conjugate_Gradient(0.01, FR.initialValue));
            Console.WriteLine();
            Console.WriteLine("Exercise from Slide 82");
            Console.WriteLine();
            Console.WriteLine(Ex_Slide82.Conjugate_Gradient(0.001, Ex_Slide82.initialValue));
            Console.WriteLine();
            Console.WriteLine("//////////////PRP/////////////");
            Console.WriteLine();
            Console.WriteLine(PRP.Conjugate_Gradient(0.01, PRP.initialValue));


            Console.ReadKey();

        }
    }
}
