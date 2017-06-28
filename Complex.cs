using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public struct Complex
    {
        public double Real { get; }
        public double Imaginary { get; }
        
        public Complex(double _real, double _imaginary)
        {
            Real = _real;
            Imaginary = _imaginary;
        }

        public static Complex operator + (Complex left, Complex right)
        {
            return new Complex(left.Real + right.Real, left.Imaginary + right.Imaginary);
        }

        public static Complex operator - (Complex left, Complex right)
        {
            return new Complex(left.Real - right.Real, left.Imaginary - right.Imaginary);
        }

        public static Complex operator * (Complex left, Complex right)
        {
            return new Complex(left.Real * right.Real, left.Imaginary * right.Imaginary);
        }

        public static Complex operator / (Complex left, Complex right)
        {
            return new Complex(left.Real / right.Real, left.Imaginary / right.Imaginary);
        }

        public static Complex operator % (Complex left, Complex right)
        {
            return new Complex(left.Real % right.Real, left.Imaginary % right.Imaginary);
        }

        public static Complex operator + (Complex left)
        {
            return new Complex(left.Real, left.Imaginary);
        }

        public static Complex operator - (Complex left)
        {
            return new Complex(-left.Real, -left.Imaginary);
        }

        public static bool operator == (Complex left, Complex right)
        {
            if (left.Real == right.Real && left.Imaginary == right.Imaginary)
                return true;
            return false;
        }

        public static bool operator != (Complex left, Complex right)
        {
            if (left.Real != right.Real || left.Imaginary != right.Imaginary)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Complex))
                return false;
            Complex complex = (Complex)obj;

            return Real.Equals(complex.Real) && Imaginary.Equals(complex.Imaginary);
        }

        public override int GetHashCode()
        {
            return Real.GetHashCode()*127 ^ Imaginary.GetHashCode();
        }

        public override string ToString()
        {
            return $"({Real}, {Imaginary}i)";
        }

        public static void main()
        {
            Complex comp1 = new Complex(2.4d, 4.4d);
            Complex comp2 = new Complex(2.5d, 3.4d);
            Console.WriteLine((comp1 + comp2 * comp2 / comp1 % comp2).ToString());
            Console.WriteLine(comp1 == comp2);
            Console.WriteLine(comp1 != comp2);
            Console.WriteLine(comp1.GetHashCode());
            Console.WriteLine(comp2.GetHashCode());
            Console.WriteLine(comp1.Equals(comp2));
            // if (ReferenceEquals(null, obj)) return false;
            // return obj is Sample && Equals((Sample) obj);
        }
    }
}
