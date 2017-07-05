using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;


namespace RootApp
{
    class Root
    {
        public delegate double Function(double x);
        public delegate double Bisection(Function f, double a, double b, double epsilion);

        static double F(double x)
        {
            return 2 * x * x + x - 9.0;
        }
        
        public static double BisectionMethod(Function f, double x1, double x2, double epsilion)
        {
            while (x2 - x1 > epsilion)
            {
                double c = (x1 + x2) / 2;
                if (F(c) >= 0) x2 = c;
                else x1 = c; 
            }
            return (x1 + x2) / 2;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Main Start");
            
            Bisection bisection = BisectionMethod;
            IAsyncResult ar = bisection.BeginInvoke(F, -3, 2, 0.001, null, null);
            Console.WriteLine("Main End");
            Console.WriteLine(bisection.EndInvoke(ar));

        }
    }
}
