using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtonsMethod
{
    class Program
    {
        public delegate double function(double x);
        const double e = Math.E;

        //First formula
        public static double FormulaOne(double x){return Math.Pow(e, x) - 3 * (Math.Pow(x, 2.0));}

        //Derivative of first formula
        public static double FormOneDeriv(double x){return Math.Pow(e, x) - 6 * x;}

        //Second formula
        public static double FormulaTwo(double x){return Math.Log(Math.Pow(x, 2)) + x - 5;}

        //Derivative of Second formula
        public static double FormTwoDeriv(double x){return (x + 2)/x;}

        //Third formula
        public static double FormulaThree(double x) { return 4*Math.Pow(x, 3) - 1 - Math.Pow(e, Math.Pow(x, 2)/2);}

        //Derivative of third formula
        public static double FormThreeDeriv(double x) { return -(Math.Pow(e, Math.Pow(x, 2) / 2) - 12*x)*x;}

        //Newtons Method Algorithm
        public static double Algorithm(function f, function fp, int nmax, double tol, double xzero)
        {
            double xone = 0.0;

            for (int n = 0; (Math.Abs(xzero - xone)) > tol || n < nmax; n++)
            {
                if (f(xzero) != 0.0 && fp(xzero) != 0.0)
                {                
                    xzero = xzero - f(xzero) / fp(xzero);
                    xone = xzero;
                }
            }

            return xzero;
        }

        static void Main(string[] args)
        {
            //First problem
            //Root one
            Console.WriteLine("Initial Guess = -0.58   N = 10");
            Console.WriteLine("The first intersection point of e^x - 3x^2 is: " + Algorithm(FormulaOne, FormOneDeriv, 10, Math.Pow(10, -6), -0.58) + "\n");

            //Root two
            Console.WriteLine("Initial Guess = 0.79   N = 4");
            Console.WriteLine("The second intersection point of e^x - 3x^2 is: " + Algorithm(FormulaOne, FormOneDeriv, 4, Math.Pow(10, -6), 0.79) + "\n");

            //Root three
            Console.WriteLine("Initial Guess = 3.71   N = 4");
            Console.WriteLine("The third intersection point of e^x - 3x^2 is: " + Algorithm(FormulaOne, FormOneDeriv, 4, Math.Pow(10, -6), 3.71) + "\n");

            //Second problem
            Console.WriteLine("Initial Guess = 2.5   N = 4");
            Console.WriteLine("The root of ln(x^2) + x - 5 is: " + Algorithm(FormulaTwo, FormTwoDeriv, 4, Math.Pow(10, -6), 2.5) + "\n");

            //Third problem
            Console.WriteLine("Initial Guess = 2.0   N = 5");
            Console.WriteLine("The root of 4x^3 - 1 - e^((x^2)/2) is: " + Algorithm(FormulaThree, FormThreeDeriv, 5, Math.Pow(10, -5), 2.0) + "\n");

            //Third problem
            Console.WriteLine("Initial Guess = 2.6   N = 23");
            Console.WriteLine("The root of 4x^3 - 1 - e^((x^2)/2) is: " + Algorithm(FormulaThree, FormThreeDeriv, 23, Math.Pow(10, -5), 2.6) + "\n");

            Console.ReadLine();


        }
    }
}
