using System;

namespace Quasi_Newton
{
    /*
     * Does not work!!!!
     * 
     * 
     */
    class Program
    {
        public static double[] initialValue = { 1, 1 };
        //partial derivative after x1
        public static double X1der(double x1, double x2)
        {
            return 2*(x1 - x2 - 2);
        }
        //partial derivative after x2
        public static double X2der(double x1, double x2)
        {
            return (4 * x2) - (2 * x1);
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
            return 1;
        }
        public static void ToString(double[] s)
        {
            Console.WriteLine(s[0]);
            Console.WriteLine(s[1]);
        }

        public static double[] computedValue = new double[2];
        //Calc gradient
        public static double[] Gradient(double x1, double x2)
        {
            double derx1 = X1der(x1, x2);
            double derx2 = X2der(x1, x2);

            computedValue[0] = derx1;
            computedValue[1] = derx2;

            return computedValue;
        }
        //calc gradient length
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
        //help with calculations
        public static double[,] calc(double[,] Hk, double[] yk, double[] sk)
        {
            double[,] result = new double[2, 2];
            double y = ((yk[0] * yk[0]) + (yk[1] * yk[1]));
            double s = ((sk[0] * sk[0]) + (sk[1] * sk[1]));
            result[0, 0] = Hk[0, 0] - ((((Hk[0, 0] * Hk[0, 0]) * y) / (Hk[0, 0] * y)) + (s / ((yk[0] * sk[0]) + (yk[1] * sk[1]))));
            result[0, 1] = Hk[0, 1] - ((((Hk[0, 1] * Hk[0, 1]) * y) / (Hk[0, 1] * y)) + (s / ((yk[0] * sk[0]) + (yk[1] * sk[1]))));
            result[1, 0] = Hk[1, 0] - ((((Hk[1, 0] * Hk[1, 0]) * y) / (Hk[1, 0] * y)) + (s / ((yk[0] * sk[0]) + (yk[1] * sk[1]))));
            result[1, 1] = Hk[1, 1] - ((((Hk[1, 1] * Hk[1, 1]) * y) / (Hk[1, 1] * y)) + (s / ((yk[0] * sk[0]) + (yk[1] * sk[1]))));
            return result;

        }
        //creates hessian
        public static double[,] Hessian()
        {
            double[,] hess = new double[2, 2];

            hess[0, 0] = X1derX1(0, 0);
            hess[0, 1] = X1derX2(0, 0);
            hess[1, 0] = X2derX1(0, 0);
            hess[1, 1] = X2derX2(0, 0);

            return hess;


        }
        public static double Function(double x1, double x2)
        {
            return (x1 * x1) + (2 * (x2 * x2)) - (2 * x1 * x2) - (4 * x1);
        }
        public static double Function(double x)
        {
            double s1;
            double s2;

            s1 = initialValue[0] + computedValue[0] * x;
            s2 = initialValue[1] + computedValue[1] * x;

            return Function(s1, s2);
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
        public static string QuasiNewton(double Eps, double[] initialValue)
        {
            double[] Pk = new double[2];
            double[] gk = new double[2];
            double[] sk = new double[2];
            double[] yk = new double[2];
            double[,] GK = new double[2, 2];
            double[,] GKs = new double[2, 2];
            double[] xk = new double[2];
            double[] gks = new double[2];
            double gradLength = GradientLen(Gradient(initialValue[0], initialValue[1]));
            int itercounter = 0;
            double lambda;
            //Step 1
            GK = Hessian();
            gk[0] = Gradient(initialValue[0], initialValue[1])[0];
            gk[1] = Gradient(initialValue[0], initialValue[1])[1];


            while (gradLength > Eps)
            {
                GKs = Hessian();
                gks[0] = Gradient(initialValue[0], initialValue[1])[0];
                gks[1] = Gradient(initialValue[0], initialValue[1])[1];
                itercounter++;
                xk[0] = initialValue[0];
                xk[1] = initialValue[1];
                //Step 2
                Pk[0] = MultiplyMatrix(GK, gk)[0];
                Pk[1] = MultiplyMatrix(GK, gk)[1];
                //Step 3
                computedValue[0] = -Gradient(initialValue[0], initialValue[1])[0];
                computedValue[1] = -Gradient(initialValue[0], initialValue[1])[1];
                lambda = Golden(0.2, 0.4, Eps);
                //Step 4
                initialValue[0] = xk[0] + lambda * Pk[0];
                initialValue[1] = xk[1] + lambda * Pk[1];
                //Step 5
                gk[0] = Gradient(initialValue[0], initialValue[1])[0];
                gk[1] = Gradient(initialValue[0], initialValue[1])[1];

                gradLength = GradientLen(Gradient(initialValue[0], initialValue[1]));

                if (gradLength <= Eps)
                {
                    Console.WriteLine("Result: ");
                    Console.WriteLine();
                    Console.WriteLine(initialValue[0]);
                    Console.WriteLine(initialValue[1]);
                    Console.WriteLine();
                    return "Itterations: " + itercounter;

                }
                sk[0] = initialValue[0] - xk[0];
                sk[1] = initialValue[1] - xk[1];
                yk[0] = gk[0] - gks[0];
                yk[1] = gk[1] - gks[1];
                GK[0, 0] = calc(GKs, yk, sk)[0, 0];
                GK[1, 0] = calc(GKs, yk, sk)[1, 0];
                GK[0, 1] = calc(GKs, yk, sk)[0, 1];
                GK[1, 1] = calc(GKs, yk, sk)[1, 1];



            }


            Console.WriteLine("Result: ");
            Console.WriteLine();
            Console.WriteLine(initialValue[0]);
            Console.WriteLine(initialValue[1]);
            Console.WriteLine();
            return "Itterations: " + itercounter;
        }
        //Main algorithms
        static void Main(string[] args)
        {

            Console.WriteLine(QuasiNewton(0.009, initialValue));
            Console.ReadKey();
        }
    }
}
