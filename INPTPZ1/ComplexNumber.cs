using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPTPZ1
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginaryPart = 0
        };
        public bool IsComplexZero()
        {
            ComplexNumber a = this;
            if (a.RealPart == 0 && a.ImaginaryPart == 0)
                return true;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber a = obj as ComplexNumber;
                return a.RealPart == RealPart && a.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }
        public ComplexNumber Multiply(ComplexNumber complexNumber2)
        {
            ComplexNumber complexNumber1 = this;
            return new ComplexNumber()
            {
                RealPart = complexNumber1.RealPart * complexNumber2.RealPart - complexNumber1.ImaginaryPart * complexNumber2.ImaginaryPart,
                ImaginaryPart = complexNumber1.RealPart * complexNumber2.ImaginaryPart + complexNumber1.ImaginaryPart * complexNumber2.RealPart
            };
        }

        public ComplexNumber Add(ComplexNumber complexNumber2)
        {
            ComplexNumber complexNumber1 = this;
            return new ComplexNumber()
            {
                RealPart = complexNumber1.RealPart + complexNumber2.RealPart,
                ImaginaryPart = complexNumber1.ImaginaryPart + complexNumber2.ImaginaryPart
            };
        }
        public ComplexNumber Subtract(ComplexNumber complexNumber2)
        {
            ComplexNumber complexNumber1 = this;
            return new ComplexNumber()
            {
                RealPart = complexNumber1.RealPart - complexNumber2.RealPart,
                ImaginaryPart = complexNumber1.ImaginaryPart - complexNumber2.ImaginaryPart
            };
        }

        internal ComplexNumber Divide(ComplexNumber complexNumber)
        {
            ComplexNumber numerator = this.Multiply(new ComplexNumber() { RealPart = complexNumber.RealPart, ImaginaryPart = -complexNumber.ImaginaryPart });
            double denominator = complexNumber.RealPart * complexNumber.RealPart + complexNumber.ImaginaryPart * complexNumber.ImaginaryPart;

            return new ComplexNumber()
            {
                RealPart = numerator.RealPart / denominator,
                ImaginaryPart = numerator.ImaginaryPart / denominator
            };
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }
    }
}
