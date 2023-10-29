using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damped_Newton
{
    public  class Demo
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Exercise [Slide 21]:    f(x) = (x1 - 4)^4 + (x1 - 2)^2*(x2)^2 + (x2 + 1)^2    x0 = (1, 1)^T ");           
            Console.WriteLine();
            Console.WriteLine(Ex_Slide21.DampedNewton(0.01, Ex_Slide21.initialValue));
            Console.WriteLine();
            Console.WriteLine("Exercise 1:    f(x) = (x1^2) / 2 +(9 * (x2^2)) / 2      x0 = (9, 1)^T ");
            Console.WriteLine();
            Console.WriteLine(Exercise1.DampedNewton(0.001, Exercise1.inittialValue));
            Console.ReadKey();
        }
    }
}
