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

        static double F1(double x)
        {
            return 2 * x * x + x - 9.0;
        }

        static double F2(double x)
        {
            Thread.Sleep(100);
            return 8 * x * x - 2 * x - 5.3;
        }

        static double F3(double x)
        {
            return -3 * x * x + x + 4.2;
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
            Console.WriteLine(f.Method.Name + " End");
            return x2 - (x2 - x1) * f(x2) / (f(x2) - f(x1));
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Main Start");

            Thread thread1 = new Thread(new ThreadStart(() =>
            {
                Function d = F1;
                IAsyncResult ar = d.BeginInvoke(BisectionMethod(F1, -3, 2, 0.001), null, null);
                Console.WriteLine(d.EndInvoke(ar));
            }));

            Thread thread2 = new Thread(new ThreadStart(() =>
            {
                Function d = F2;
                IAsyncResult ar = d.BeginInvoke(BisectionMethod(F2, -2, 5, 0.001), null, null);
                Console.WriteLine(d.EndInvoke(ar));
            }));

            Thread thread3 = new Thread(new ThreadStart(() =>
            {
                Function d = F3;
                IAsyncResult ar = d.BeginInvoke(BisectionMethod(F3, -1, 4, 0.001), null, null);
                Console.WriteLine(d.EndInvoke(ar));
            }));

            thread1.Start();
            thread2.Start();
            thread3.Start();

            Thread.Sleep(1);
            Console.WriteLine("Main End");
        }
    }
}
