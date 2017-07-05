using System;

namespace ComplexApp
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

        public void Addition(Complex complex)
        {
            this += complex;
        }

        public void Addition(params Complex[] comps)
        {
            foreach (Complex comp in comps)
            {
                this += comp;
            }
        }

        public static Complex AdditionStatic(Complex left, Complex right)
        {
            return left + right;
        }

        public static Complex AdditionStatic(params Complex[] comps)
        {
            Complex complex = new Complex(0.0d, 0.0d);
            foreach (Complex comp in comps)
            {
                complex += comp;
            }
            return complex;
        }

        public void Subtraction(Complex complex)
        {
            this -= complex;
        }

        public static Complex SubtractionStatic(Complex left, Complex right)
        {
            return left - right;
        }

        public void Multiplication(Complex complex)
        {
            this *= complex;
        }

        public void Multiplication(params Complex[] comps)
        {
            foreach (Complex comp in comps)
            {
                this *= comp;
            }
        }

        public static Complex MultiplicationStatic(Complex left, Complex right)
        {
            return left * right;
        }

        public static Complex MultiplicationStatic(params Complex[] comps)
        {
            Complex complex = new Complex(0.0d, 0.0d);
            foreach (Complex comp in comps)
            {
                complex *= comp;
            }
            return complex;
        }

        public void Division(Complex complex)
        {
            this /= complex;
        }

        public static Complex DivisionStatic(Complex left, Complex right)
        {
            return left / right;
        }

        public static double Modulus(Complex complex)
        {
            return (double) complex;
        }

        public static explicit operator double(Complex complex)
        {
            return Math.Sqrt(complex.Real * complex.Real + complex.Imaginary * complex.Imaginary);
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
            return new Complex(left.Real * right.Real - left.Imaginary * right.Imaginary, left.Real * right.Imaginary + left.Imaginary * right.Real);
        }

        public static Complex operator / (Complex left, Complex right)
        {
            return new Complex((left.Real * right.Real + left.Imaginary * right.Imaginary) / (right.Real * right.Real + right.Imaginary * right.Imaginary), (right.Real * left.Imaginary - left.Real * right.Imaginary) / (right.Real * right.Real + right.Imaginary * right.Imaginary));
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
            if (left.Real.Equals(right.Real) && left.Imaginary.Equals(right.Imaginary))
                return true;
            return false;
        }

        public static bool operator != (Complex left, Complex right)
        {
            if (!left.Real.Equals(right.Real) || !left.Imaginary.Equals(right.Imaginary))
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
            return Real.GetHashCode() * 127 ^ Imaginary.GetHashCode();
        }

        public override string ToString()
        {
            return $"({Real}, {Imaginary}i)";
        }

        public static void main()
        {
            Complex comp1 = new Complex(-2, 1);
            Complex comp2 = new Complex(1, -1);
            Complex comp3 = new Complex(4, 2);
            comp3.Addition(comp1, comp2);
            double d = (double) comp3;
        }
    }
}
