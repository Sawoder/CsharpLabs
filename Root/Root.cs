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

        public static double BisectionMethod(Function f, double a, double b, double epsilon)
        {
            Console.WriteLine(f.Method.Name + " Start");
            double x1 = a;
            double x2 = b;
            double fb = f(b);
            while (Math.Abs(x2 - x1) > epsilon)
            {
                double midpt = 0.5 * (x1 + x2);
                if (fb * f(midpt) > 0)
                    x2 = midpt;
                else
                    x1 = midpt;
            }
            Thread.Sleep(1000);
            Console.WriteLine(f.Method.Name + " End");
            return x2 - (x2 - x1) * f(x2) / (f(x2) - f(x1));
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
