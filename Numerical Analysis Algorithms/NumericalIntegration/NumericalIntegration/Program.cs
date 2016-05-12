using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalIntegration
{
    class Program
    {
        public delegate double function(double x);

        //First Formula
        public static double FormulaOne(double x) { return 1/(1 + Math.Pow(x, 2)); }

        //Second Formula
        public static double FormulaTwo(double x) { return Math.Pow(x, 2) * Math.Sin(x); }

        //Example
        public static double Example(double x) { return Math.Log(x); }

        //Composite Simpson's Rule
        public static double Simpson(double a, double b, function f, int n)
        {
            double approx;
            double h = (b - a) / (2 * n);
            double[] values = new double[2*n + 1];
            int i = 0;
            double evenSum = 0;
            double oddsum = 0;

            values[0] = a;
            values[2*n] = b;

            for(i = 1; i < n; i++)
            {
                values[2*i] += a + (2*i*h);
                evenSum += f(values[2 * i]);
            }

            for (i = 1; i <= n; i++ )
            {
                values[(2 * i) - 1] += a + (((2 * i) - 1) * h);
                oddsum += f(values[(2 * i) - 1]);
            }

            approx = (h / 3) * (f(a) + f(b) + 4 * oddsum + 2 * evenSum);

            return approx;
        }

        //Composite Midpoint Rule
        public static double Midpoint(double a, double b, function f, int n)
        {
            double approx = 0;
            double h = (b - a) / n;
            double[] values = new double[n + 1];
            double[] wValues = new double[n + 1];
            double sum = 0;
            int i = 0;

            for(i = 0; i <= n; i++)
            {
                values[i] = f(a + i*h);
            }

            for(i = 1; i <= n; i++)
            {
                wValues[i] = (values[i] + values[i-1]) / 2;
                sum += wValues[i];
            }

            approx = h*sum;

            return approx;
        }

        static void Main(string[] args)
        {
            double a;
            double b;
            int n;

            //Simpson's Rule
            Console.WriteLine("Using Composite Simpson's Rule:");
            Console.WriteLine();

            //Fomula 1
            Console.WriteLine("1/(1 + x^2)");
            Console.WriteLine("Enter a for (a, b): ");
            a = Double.Parse(Console.ReadLine());
            Console.WriteLine("Enter b for (a, b): ");
            b = Double.Parse(Console.ReadLine());
            Console.WriteLine("Enter n: ");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("h = " + (b - a) / (2 * n));
            Console.Write("Approximated integral value of (1/(1 + x^2): " + Simpson(a, b, FormulaOne, n));       
            Console.WriteLine();
            Console.WriteLine();

            //Formula 2
            Console.WriteLine("(x^2)sin(x)");
            Console.WriteLine("Enter a for (a, b): ");
            a = Double.Parse(Console.ReadLine());
            Console.WriteLine("Enter b for (a, b): ");
            b = Double.Parse(Console.ReadLine());
            Console.WriteLine("Enter n: ");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("h = " + (b - a) / (2 * n));
            Console.Write("Approximated integral value of (x^2)sin(x): " + Simpson(a, b, FormulaTwo, n));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            //Midpoint Rule
            Console.WriteLine("Using Composite Midpoint Rule:");
            Console.WriteLine();

            //Formula 1
            Console.WriteLine("1/(1 + x^2)");
            Console.WriteLine("Enter a for (a, b): ");
            a = Double.Parse(Console.ReadLine());
            Console.WriteLine("Enter b for (a, b): ");
            b = Double.Parse(Console.ReadLine());
            Console.WriteLine("Enter n: ");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("h = " + (b - a) / n);
            Console.Write("Approximated integral value of (1/(1 + x^2): " + Midpoint(a, b, FormulaOne, n));
            Console.WriteLine();
            Console.WriteLine();

            //Formula 2
            Console.WriteLine("(x^2)sin(x)");
            Console.WriteLine("Enter a for (a, b): ");
            a = Double.Parse(Console.ReadLine());
            Console.WriteLine("Enter b for (a, b): ");
            b = Double.Parse(Console.ReadLine());
            Console.WriteLine("Enter n: ");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("h = " + (b - a) / n);
            Console.Write("Approximated integral value of (x^2)sin(x): " + Midpoint(a, b, FormulaTwo, n));

             

            Console.ReadLine();
        }
    }
}
