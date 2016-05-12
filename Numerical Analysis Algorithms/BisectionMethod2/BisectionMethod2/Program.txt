using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisectionMethod2
{
    class Program
    {
        static void Main(string[] args)
        {
            double a, b, c, TOL, currentTOL;
            int N, NMax;


            //First Problem
            //Set initial values
            N = 0;
            NMax = 10;
            a = 0;
            b = 1;
            TOL = 0.5 * Math.Pow(10, -3);
            
            //Use Algorithm
            while(N <= NMax)
            {
                c = (a + b) / 2;
                currentTOL = (b - a) / 2;

                if((Math.Sin(c) - 5 + (6 * c)) == 0 || currentTOL < TOL)
                {
                    Console.WriteLine("The root for sin(x)-5+6x is about: " + c + "\nInterval: [0,1]   Iterations: 10\n\n");
                    goto Second;
                }

                N++;

                if((Math.Sin(a) - 5 + (6 * a)) * (Math.Sin(c) - 5 + (6 * c)) < 0)
                {
                    b = c;
                }
                else
                {
                    a = c;
                }
            }
            Console.WriteLine("Max number of iterations reached.");
            Console.ReadKey();


            Second:
            //Second Problem
            //Set initial values
            N = 0;
            NMax = 20;
            a = 6;
            b = 7;
            TOL = 0.5 * Math.Pow(10, -6);

            //Use Algorithm
            while (N <= NMax)
            {
                c = (a + b) / 2;
                currentTOL = (b - a) / 2;

                if ((Math.Pow(Math.Cos(c), 2) + 6 - c) == 0 || currentTOL < TOL)
                {
                    Console.WriteLine("The root for cos^2(x)+6-x is about: " + c + "\nInterval: [6,7]   Iterations: 20\n\n");
                    goto Third;
                }

                N++;

                if ((Math.Pow(Math.Cos(a), 2) + 6 - a) * (Math.Pow(Math.Cos(c), 2) + 6 - a) < 0)
                {
                    b = c;
                }
                else
                {
                    a = c;
                }
            }
            Console.WriteLine("Max number of iterations reached.");
            Console.ReadKey();


            Third:
            //Third Problem
            //Set initial values
            N = 0;
            NMax = 13;
            a = 2;
            b = 3;
            TOL = 1 * Math.Pow(10, -4);

            //Use Algorithm
            while (N <= NMax)
            {
                c = (a + b) / 2;
                currentTOL = (b - a) / 2;

                if ((Math.Pow(c, 3) - 21) == 0 || currentTOL < TOL)
                {
                    Console.WriteLine("The root for (x^3) - 21 is about: " + c + "\nInterval: [2,3]   Iterations: 13\n\n");
                    goto Fourth;
                }

                N++;

                if ((Math.Pow(a, 3) - 21) * (Math.Pow(c, 3) - 21) < 0)
                {
                    b = c;
                }
                else
                {
                    a = c;
                }
            }
            Console.WriteLine("Max number of iterations reached.");
            Console.ReadKey();

            Fourth:
            //Fourth Problem
            //Set initial values
            N = 0;
            NMax = 20;
            a = 1;
            b = 2;
            TOL = 0.5 * Math.Pow(10, -6);

            //Use Algorithm
            while (N <= NMax)
            {
                c = (a + b) / 2;
                currentTOL = (b - a) / 2;

                if ((Math.Sin(c)-(Math.Pow(c, 3) + 5)) == 0 || currentTOL < TOL)
                {
                    Console.WriteLine("The root for sin(x) - (x^3) + 5 is about: " + c + "\nInterval: [1,2]   Iterations: 20\n\n");
                    goto Fifth;
                }

                N++;

                if ((Math.Sin(a) - (Math.Pow(a, 3)) + 5) * (Math.Sin(c) - (Math.Pow(c, 3)) + 5) < 0)
                {
                    b = c;
                }
                else
                {
                    a = c;
                }
            }
            Console.WriteLine("Max number of iterations reached.");

            Fifth:
            Console.ReadKey();
        }
    }
}
