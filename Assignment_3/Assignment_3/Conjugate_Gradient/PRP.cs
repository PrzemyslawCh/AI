using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conjugate_Gradient
{

    /*
     * Works worst then FR for same Fucntion
     * Error exist somewhere
     * 
     */

    class PRP
    {
        public static double Function(double x)
        {
            double s1;
            double s2;

            s1 = initialValue[0] + computedValue[0] * x;
            s2 = initialValue[1] + computedValue[1] * x;

            return Function(s1, s2);
        }
        //Equation
        public static double Function(double x1, double x2)
        {
            return x1 - x2 + 2 * (x1 * x1) + 2 * (x1 * x2) + x1 * x2;
        }
        //partial derivative after x1
        static double X1der(double x1, double x2)
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

        public static string Conjugate_Gradient(double Eps, double[] initialValue)
        {
            double lambda;
            double[] Pk = new double[2];
            double[] gk = new double[2];
            double[] gku = new double[2];
            double[] result = new double[2];
            double gradLength = GradientLen(Gradient(initialValue[0], initialValue[1]));
            int itercounter = 0;
            double Bk = 0;
            int n = 2; // dont know what is n? is it from n columns?

            //Pk[0] = Gradient(initialValue[0], initialValue[1])[0];
            //Pk[1] = Gradient(initialValue[0], initialValue[1])[1];

            do
            {



                gk[0] = Gradient(initialValue[0], initialValue[1])[0];
                gk[1] = Gradient(initialValue[0], initialValue[1])[1];

                //Console.WriteLine("gk values: ");
                //ToString(gk);

                if (itercounter % n == 0)
                {
                    Pk[0] = -Gradient(initialValue[0], initialValue[1])[0];
                    Pk[1] = -Gradient(initialValue[0], initialValue[1])[1];
                }
                else
                {
                    Pk[0] = -Gradient(initialValue[0], initialValue[1])[0] + (Bk * Pk[0]);
                    Pk[1] = -Gradient(initialValue[0], initialValue[1])[1] + (Bk * Pk[1]);  
                }

                computedValue[0] = -Gradient(initialValue[0], initialValue[1])[0];
                computedValue[1] = -Gradient(initialValue[0], initialValue[1])[1];
              
                lambda = Golden(0, 1, Eps);

                initialValue[0] += lambda * Pk[0];
                initialValue[1] += lambda * Pk[1];

                gku[0] = Gradient(initialValue[0], initialValue[1])[0];
                gku[1] = Gradient(initialValue[0], initialValue[1])[1];

                //Console.WriteLine("PK values: ");
                //ToString(initialValue);

                Bk = ((gku[0] * gku[0]) + (gku[1] * gku[1])) / ((gk[0] * gk[0]) + (gk[1] * gk[1]));

                gradLength = GradientLen(Gradient(initialValue[0], initialValue[1]));

                itercounter++;

            }
            while (gradLength > Eps);

            Console.WriteLine("Result: ");
            Console.WriteLine();
            Console.WriteLine(initialValue[0]);
            Console.WriteLine(initialValue[1]);
            Console.WriteLine();
            return "Itterations: " + itercounter;
        }
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
        
        
    }
}
