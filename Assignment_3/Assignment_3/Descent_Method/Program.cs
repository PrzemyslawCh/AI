using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

/*
 * Implementation of Gradient Descent algorithm
 * X1der and X2der are derivatives calculated by hands
 * Works well for: 
 * f(x) = x1 - x2 + (2x1)^2 + 2x1x2 + (x2)^2   
 * x0 = (0,0) ^ T
 * You can easly uncomment "Console.Writelines" to check how every itternation looks
 */
namespace GradientDescent
{
    
    public class Program
    {
        
        public static double la;
        
        //Equation
        public static double Function(double x1, double x2)
        {
            return x1 - x2 + 2*(x1 * x1) + 2 * (x1 * x2) + x1 * x2;
        }
        //partial derivative after x1
        static double X1der(double x1,double x2)
        {
            return 4 * x1 + 2 * x2 + 1;
        }
        //partial derivative after x2
        static double X2der(double x1, double x2)
        {
            return 2 * x1 + 2 * x2 - 1;
        }
        public static double[] initialValue = { 0, 0 };
        public static double[] computedValue = new double[2];
        
        //gradient calc
        static double[] Gradient(double x1, double x2)
        {
            double derx1 = X1der(x1, x2);
            double derx2 = X2der(x1, x2);
            
            computedValue[0] = derx1;
            computedValue[1] = derx2;

            return computedValue;
        }
        //Calculating gradient length
        static double GradientLen(double[] gradmatrix)
        {
            double result = 0;
            for (int i = 0; i < gradmatrix.Length; i++)
            {
                result += Math.Pow(gradmatrix[i], 2);
            }
            return Math.Sqrt(result);
        }
        //To show steps
        static void ToString(double[] s)
        {
            Console.WriteLine(s[0]);
            Console.WriteLine(s[1]);
        }
        //Creates Function which is compatible with Golden_Section
        public static double Function(double x)
        {
            double s1;
            double s2;

            s1 = initialValue[0] + computedValue[0] * x;
            s2 = initialValue[1] + computedValue[1] * x;

            return Function(s1, s2);
        }     
        //Main algorithms
        static string Gradient_Descent(double Eps, double[] initialValue)
        {
            double[] Pk = new double[2];
            double[] result = new double[2];
            double lambda;
            double start = GradientLen(Gradient(initialValue[0], initialValue[1]));
            int itercounter = 0;
            
            while (start > Eps)
            {
                itercounter++;
                //calculating pk-s
                Pk[0] = -Gradient(initialValue[0], initialValue[1])[0];
                Pk[1] = -Gradient(initialValue[0], initialValue[1])[1];

                computedValue[0] = -Gradient(initialValue[0], initialValue[1])[0];
                computedValue[1] = -Gradient(initialValue[0], initialValue[1])[1];
                //Console.WriteLine("PK values: ");
                //ToString(Pk);
                //x*
                result[0] = initialValue[0];
                result[1] = initialValue[1];

                //Console.WriteLine("Xk values: ");
                //ToString(result);
                //Here we have to minimise lamda equation
                //Console.WriteLine("Lambda: ");
                lambda = Golden(0, 1, Eps);
                //Console.WriteLine(lambda);

                initialValue[0] = result[0] + lambda * Pk[0];
                initialValue[1] = result[1] + lambda * Pk[1];

                //Console.WriteLine("Xk + 1 values: ");
                //ToString(initialValue);

                start = GradientLen(Gradient(initialValue[0], initialValue[1]));
            }
            Console.WriteLine("Result: ");
            Console.WriteLine();
            Console.WriteLine(result[0]);
            Console.WriteLine(result[1]);
            Console.WriteLine();
            return "Itterations: " + itercounter;
            


        }
        //Golden sectiong to find a minimum 
        public static double Golden(double a, double b, double Eps)
        {
            double Ta = ((1 + Math.Sqrt(5)) / 2) - 1;

            double x2 = a + Ta * (b - a);

            double x1 = a + (1 - Ta) * (b - a);

            while (Math.Abs(b - a) > Eps)
            {
                if (Function(x1) < Function(x2))
                {
                    b = x2;
                    x2 = x1;
                    x1 = a + (1 - Ta) * (b - a);

                }
                else if (Function(x1) == Function(x2))
                {
                    a = x1;
                    b = x2;
                    x2 = a + Ta * (b - a);
                    x1 = a + (1 - Ta) * (b - a);

                }
                else
                {
                    a = x1;
                    x1 = x2;
                    x2 = a + Ta * (b - a);

                }
            }
            return (a + b) / 2;
        }

        static void Main(string[] args)
        {                    
            Console.WriteLine();
           
            Console.WriteLine(Gradient_Descent(0.009,initialValue));

            Console.ReadKey();
        }
    }   
}