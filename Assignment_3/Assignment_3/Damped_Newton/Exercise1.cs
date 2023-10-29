using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damped_Newton
{
    public class Exercise1
    {

        
        public static double[] inittialValue = { 9, 1 };
        //partial derivative after x1
        public static double X1der(double x1, double x2)
        {
            return x1;
        }
        //partial derivative after x2
        public static double X2der(double x1, double x2)
        {
            return 9 * x2;
        }
        public static double X1derX1(double x1, double x2)
        {
            return 1;
        }
        public static double X1derX2(double x1, double x2)
        {
            return 0;
        }
        public static double X2derX1(double x1, double x2)
        {
            return 0;
        }
        public static double X2derX2(double x1, double x2)
        {
            return (double)1 / 9;
        }
        public  static void ToString(double[] s)
        {
            Console.WriteLine(s[0]);
            Console.WriteLine(s[1]);
        }

        public static double[] computedValue = new double[2];

        public static double[] Gradient(double x1, double x2)
        {
            double derx1 = X1der(x1, x2);
            double derx2 = X2der(x1, x2);

            computedValue[0] = derx1;
            computedValue[1] = derx2;

            return computedValue;
        }
        public static double GradientLen(double[] gradmatrix)
        {
            double result = 0;
            for (int i = 0; i < gradmatrix.Length; i++)
            {
                result += Math.Pow(gradmatrix[i], 2);
            }
            return Math.Sqrt(result);
        }
        public static double[] MultiplyMatrix(double[,] Gk, double[] pk)
        {
            double[] res = new double[2];
            res[0] = Gk[0, 0] * pk[0] + Gk[0, 1] * pk[1];
            res[1] = Gk[1, 0] * pk[0] + Gk[1, 1] * pk[1];
            return res;

        }
        public static double[,] Hessian(double[] gradient)
        {
            double[,] hess = new double[2, 2];

            hess[0, 0] = X1derX1(0, 0);
            hess[0, 1] = X1derX2(0, 0);
            hess[1, 0] = X2derX1(0, 0);
            hess[1, 1] = X2derX2(0, 0);

            return hess;


        }
        public static string DampedNewton(double Eps, double[] initialValue)
        {
            double[] Pk = new double[2];
            double[] gk = new double[2];
            double[,] GK = new double[2, 2];
            double[] result = new double[2];
            double gradLength = GradientLen(Gradient(initialValue[0], initialValue[1]));
            int itercounter = 0;
            GK = Hessian(Pk);
            while (gradLength > Eps)
            {
                itercounter++;

                gk[0] = Gradient(initialValue[0], initialValue[1])[0];
                gk[1] = Gradient(initialValue[0], initialValue[1])[1];

                //Console.WriteLine("gk values: ");
                //ToString(gk);

                initialValue[0] = initialValue[0] - MultiplyMatrix(GK, gk)[0];
                initialValue[1] = initialValue[1] - MultiplyMatrix(GK, gk)[1];

                //Console.WriteLine("Xk + 1 values: ");
                //ToString(initialValue);

                gradLength = GradientLen(Gradient(initialValue[0], initialValue[1]));

            }
            Console.WriteLine("Result: ");
            Console.WriteLine();
            Console.WriteLine(initialValue[0]);
            Console.WriteLine(initialValue[1]);
            Console.WriteLine();
            return "Itterations: " + itercounter;



        }
        
        
    }
}
