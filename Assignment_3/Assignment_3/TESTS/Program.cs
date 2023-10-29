using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTS
{

    //TEST Gradient_Descent
    //internal class Program
    //{

    //    public static double[] computedValue = new double[2];
    //    public static double[] initialValue = { 9, 1 };
    //    //partial derivative after x1
    //    static double X1der(double x1, double x2)
    //    {
    //        return x1;
    //    }
    //    //partial derivative after x2
    //    static double X2der(double x1, double x2)
    //    {
    //        return 9 * x2;
    //    }
    //    static double[] Gradient(double x1, double x2)
    //    {
    //        double derx1 = X1der(x1, x2);
    //        double derx2 = X2der(x1, x2);

    //        computedValue[0] = derx1;
    //        computedValue[1] = derx2;

    //        return computedValue;
    //    }
    //    //Calculating gradient length
    //    static double GradientLen(double[] gradmatrix)
    //    {
    //        double result = 0;
    //        for (int i = 0; i < gradmatrix.Length; i++)
    //        {
    //            result += Math.Pow(gradmatrix[0], 2);
    //        }
    //        return Math.Sqrt(result);
    //    }
    //    //To show steps
    //    static void ToString(double[] s)
    //    {
    //        Console.WriteLine(s[0]);
    //        Console.WriteLine(s[1]);
    //    }

    //    public static double Function(double x)
    //    {
    //        double s1;
    //        double s2;

    //        s1 = initialValue[0] + computedValue[0] * x;
    //        s2 = initialValue[1] + computedValue[1] * x;

    //        return Function(s1, s2);
    //    }
    //    static double Function(double x1, double x2)
    //    {
    //        return ((x1 * x1) / 2) + (9 * (x2 * x2));
    //    }

    //    static string Gradient_Descent(double Eps, double[] initialValue)
    //    {
    //        double[] Pk = new double[2];
    //        double[] result = new double[2];
    //        double lambda;
    //        double start = GradientLen(Gradient(initialValue[0], initialValue[1]));
    //        int itercounter = 0;

    //        while (start > Eps)
    //        {
    //            itercounter++;
    //            //calculating pk-s
    //            Pk[0] = -Gradient(initialValue[0], initialValue[1])[0];
    //            Pk[1] = -Gradient(initialValue[0], initialValue[1])[1];

    //            //Console.WriteLine("PK values: ");
    //            //ToString(Pk);
    //            //x*
    //            result[0] = initialValue[0];
    //            result[1] = initialValue[1];

    //            //Console.WriteLine("Xk values: ");
    //            //ToString(result);
    //            //Here we have to minimise lamda equation
    //            //Console.WriteLine("Lambda: ");             
    //            lambda = Golden(0, 1, Eps);
    //            //Console.WriteLine(lambda);

    //            initialValue[0] = result[0] + lambda * Pk[0];
    //            initialValue[1] = result[1] + lambda * Pk[1];

    //            //Console.WriteLine("Xk + 1 values: ");
    //            //ToString(initialValue);

    //            start = GradientLen(Gradient(initialValue[0], initialValue[1]));
    //        }
    //        Console.WriteLine(result[0]);
    //        Console.WriteLine(result[1]);
    //        return "Itterations: " + itercounter;



    //    }
    //    //Golden sectiong to find a minimum 
    //    public static double Golden(double a, double b, double Eps)
    //    {
    //        double Ta = ((1 + Math.Sqrt(5)) / 2) - 1;

    //        double x2 = a + Ta * (b - a);

    //        double x1 = a + (1 - Ta) * (b - a);

    //        while (Math.Abs(b - a) > Eps)
    //        {
    //            if (Function(x1) < Function(x2))
    //            {
    //                b = x2;
    //                x2 = x1;
    //                x1 = a + (1 - Ta) * (b - a);

    //            }
    //            else if (Function(x1) == Function(x2))
    //            {
    //                a = x1;
    //                b = x2;
    //                x2 = a + Ta * (b - a);

    //            }
    //            else
    //            {
    //                a = x1;
    //                x1 = x2;
    //                x2 = a + Ta * (b - a);

    //            }
    //        }
    //        return (a + b) / 2;
    //    }

    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine(Gradient_Descent(1,initialValue));
    //        Console.ReadKey();
    //    }
    //}

    internal class Program
    {
        public static double[] initialValue = { 1, 1 };
        public static double[] computedValue = new double[2];
        //partial derivative after x1
        static double X1der(double x1, double x2)
        {
            return 2 *((x1 - 2) * Math.Pow(x2, 2)) + (4 * Math.Pow((x1 - 2), 3));
        }
        //partial derivative after x2
        static double X2der(double x1, double x2)
        {
            return 2 * ((Math.Pow((x1 - 2), 2) * x2) + x2 + 1);
        }
        //second partial derivative x1 after x1
        static double X1derX1(double x1, double x2)
        {
            return 2 * ((6 * Math.Pow((x1 - 2), 2)) + (x2 * x2));
        }
        //second partial derivative x1 after x2
        static double X1derX2(double x1, double x2)
        {
            return 4 * (x1 - 2) * x2;
        }
        //second partial derivative x2 after x1
        static double X2derX1(double x1, double x2)
        {
            return 4 * (x1 - 2) * x2;
        }
        //second partial derivative x2 after x2
        static double X2derX2(double x1, double x2)
        {
            return 2 * (Math.Pow((x1 - 2), 2) + 1);
        }

        static void ToString(double[] s)
        {
            Console.WriteLine(s[0]);
            Console.WriteLine(s[1]);
        }
        //Calcs Gradient
        static double[] Gradient(double x1, double x2)
        {
            double derx1 = X1der(x1, x2);
            double derx2 = X2der(x1, x2);

            computedValue[0] = derx1;
            computedValue[1] = derx2;

            return computedValue;
        }
        //Cals Lenght of gradient
        static double GradientLen(double[] gradmatrix)
        {
            double result = 0;

            for (int i = 0; i < gradmatrix.Length; i++)
            {
                result += Math.Pow(gradmatrix[i], 2);
            }
            return Math.Sqrt(result);
        }
        //Multiplies matrixes
        static double[] MultiplyMatrix(double[,] Gk, double[] pk)
        {
            double[] res = new double[2];

            res[0] = Gk[0, 0] * pk[0] + Gk[0, 1] * pk[1];
            res[1] = Gk[1, 0] * pk[0] + Gk[1, 1] * pk[1];

            return res;

        }
        //Forms Hessian
        static double[,] Hessian()
        {
            double[,] hess = new double[2, 2];

            hess[0, 0] = X1derX1(initialValue[0], initialValue[1]);
            hess[0, 1] = X1derX2(initialValue[0], initialValue[1]);
            hess[1, 0] = X2derX1(initialValue[0], initialValue[1]);
            hess[1, 1] = X2derX2(initialValue[0], initialValue[1]);

            return hess;
        }
        //Inverting matrix
        static double[,] InverseMat(double[,] matrix)
        {
            double determinant = (matrix[0, 0] * matrix[1,0]) - (matrix[1,0] * matrix[1,1]);
            double mul = (double)1 / determinant;

            double[,] result = new double[2, 2];
            result[0, 0] = matrix[1,1] * mul;
            result[0, 1] = -matrix[0, 1] * mul;
            result[1, 0] = -matrix[1, 0] * mul;
            result[1, 1] = matrix[0, 0] * mul;

            return result;

        }
        //main algorithm
        static string DampedNewton(double Eps, double[] initialValue)
        {
            
            double[] gk = new double[2];
            double[,] GK = Hessian();
            double gradLength = GradientLen(Gradient(initialValue[0], initialValue[1]));
            int itercounter = 0;
            GK = InverseMat(GK);
            while (gradLength > Eps)
            {
                itercounter++;

                gk[0] = Gradient(initialValue[0], initialValue[1])[0];
                gk[1] = Gradient(initialValue[0], initialValue[1])[1];

                //Console.WriteLine("gk values: ");
                //ToString(gk);

                initialValue[0] = initialValue[0] + MultiplyMatrix(GK, gk)[0];
                initialValue[1] = initialValue[1] + MultiplyMatrix(GK, gk)[1];

                //Console.WriteLine("Xk + 1 values: ");
                //ToString(initialValue);

                gradLength = GradientLen(Gradient(initialValue[0], initialValue[1]));

            }
            Console.WriteLine("Result: ");
            Console.WriteLine(initialValue[0]);
            Console.WriteLine(initialValue[1]);
            return "Itterations: " + itercounter;



        }
        static void Main(string[] args)
        {
            Console.WriteLine(DampedNewton(0.001, initialValue));

            Console.ReadKey();
        }
    }
}
