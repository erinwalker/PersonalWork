using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtonsDividedDifferences
{
    class Program
    {
        private const double e = Math.E;

        public delegate double function(double x);

        //First formula
        public static double FormulaOne(double x) { return 2 * Math.Sin(x) + Math.Cos(3 * x); }

        //Second formula
        public static double FormulaTwo(double x) { return Math.Pow(e, Math.Pow(-x, 2)); }

        public static void Algorithm(function f, double a, double b, double xValue, int n)
        {
            //Make variables
            double sum, temp;
            double[] x = new double[n];
            double[,] y = new double [n, n];
            int i,j,k=0,ff;
            double xx = a;
            double increment = Math.Abs(b - a) / (n + 1);

            //Sets first points before I increment
            x[0] = xx;
            y[0, 1] = f(xx);

            //Get inital points
            for(i = 1; i < n; i++)
            {
                xx += increment;
                x[i] = xx;
                y[i, k] = f(xx);
            }
            
          //Make tree using an array
          for(i = 1; i < n; i++)
          {
            k=i;
            for(j = 0; j < n-i; j++)
            {
                y[j, i]=(y[j+1, i-1]-y[j, i-1])/(x[k]-x[j]);
                k++;
            }
          }

          //Finds the range for the xValue
          i = 0;
          do
          {
              if (x[i] < xValue && xValue < x[i + 1])
                  k = 1;
              else
                  i++;
          }
          while (k != 1 && i+1 < n);
          
          //Calculates the approximated value
          ff=i;
          sum=0;
          for(i = 0; i < n-1; i++)
          {
            k=ff;
            temp=1;
            for(j = 0; j < i && k < n; j++)
            {
                temp = temp * (xValue - x[k]);
                k++;
            }
                sum += temp * (y[ff, i]);
          }

          Console.WriteLine("The approximated value is: " + sum);
          Console.WriteLine("The actual answer is: " + f(xValue));
          Console.WriteLine("The error is: " + (f(xValue) - sum));
        }

        static void Main(string[] args)
        {
            double a, b, x;
            int n;

            Console.WriteLine("f(x) = 2*sin(x) + cos(3x)");
            Console.WriteLine("Please enter a for (a, b):");
            a = Double.Parse(Console.ReadLine());
            Console.WriteLine("Please enter b for (a, b):");
            b = Double.Parse(Console.ReadLine());
            Console.WriteLine("Please enter n:");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the x value:");
            x = Double.Parse(Console.ReadLine());
            Console.WriteLine();
            
            Algorithm(FormulaOne, a, b, x, n);

            Console.WriteLine("\n");

            Console.WriteLine("f(x) = e^(-x^2)");
            Console.WriteLine("Please enter a for (a, b):");
            a = Double.Parse(Console.ReadLine());
            Console.WriteLine("Please enter b for (a, b):");
            b = Double.Parse(Console.ReadLine());
            Console.WriteLine("Please enter n:");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the x value:");
            x = Double.Parse(Console.ReadLine());
            Console.WriteLine();
          
            Algorithm(FormulaTwo, a, b, x, n);

            Console.ReadKey();
        }
    }
}
