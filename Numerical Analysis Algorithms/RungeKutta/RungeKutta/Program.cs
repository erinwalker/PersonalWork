using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RungeKutta
{
    class Program
    {
        public delegate double function(double t, double y);

        //First Function
        public static double FormulaOne(double t, double y) { return Math.Pow(t, 3) / y; }

        //Second Function
        public static double FormulaTwo(double t, double y) { return 2 *(t + 1) * y; }

        public static void RungeKutta(function f, double a, double b, double h, double initial)
        {
            int size = Convert.ToInt32(b / h);
            double[] y = new double[size + 1];
            double[] t = new double[size + 1];
            double[] k = new double[4];

            y[0] = initial;
            t[0] = a;

            for(int i = 0; t[i] != b; i++)
            {
                t[i + 1] = t[i] + h;
                k[0] = f(t[i], y[i]);
                k[1] = f((t[i] + (h/2)), (y[i] + ((h/2) * k[0])));
                k[2] = f((t[i] + (h / 2)), (y[i] + ((h/2) * k[1])));
                k[3] = f((t[i] + h), (y[i] + (h * k[2])));
                y[i + 1] = y[i] + (h / 6) * (k[0] + 2*k[1] + 2*k[2] + k[3]);
                Console.WriteLine("y(" + t[i + 1] + ") = " + y[i + 1]);
            }
        }

        static void Main(string[] args)
        {
            double a, b, h, initial;
            Console.WriteLine("Equation: (t^3)/y");
            Console.WriteLine("Input a for [a, b]: ");
            a = Double.Parse(Console.ReadLine());
            Console.WriteLine("Input b for [a, b]: ");
            b = Double.Parse(Console.ReadLine());
            Console.WriteLine("Input h: ");
            h = Double.Parse(Console.ReadLine());
            Console.WriteLine("Input initial condition for y(" + a + "): ");
            initial = Double.Parse(Console.ReadLine());

            RungeKutta(FormulaOne, a, b, h, initial);
            Console.WriteLine();
            Console.WriteLine("Equation: 2(t+1)y");
            Console.WriteLine("Input a for [a, b]: ");
            a = Double.Parse(Console.ReadLine());
            Console.WriteLine("Input b for [a, b]: ");
            b = Double.Parse(Console.ReadLine());
            Console.WriteLine("Input h: ");
            h = Double.Parse(Console.ReadLine());
            Console.WriteLine("Input initial condition for y(" + a + "): ");
            initial = Double.Parse(Console.ReadLine());

            RungeKutta(FormulaTwo, a, b, h, initial);

            Console.ReadKey();
        }
    }
}
