using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPTPZ1
{
    class Polynome
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynome()
        {
            Coefficients = new List<ComplexNumber>();
        }

        public Polynome(List<ComplexNumber> coefs)
        {
            Coefficients = new List<ComplexNumber>();
            foreach (ComplexNumber element in coefs)
            {
                Coefficients.Add(element);
            }
        }

        public Polynome Derivate()
        {
            Polynome derivation = new Polynome();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                derivation.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { RealPart = i }));
            }

            return derivation;
        }

        public ComplexNumber Evaluate(ComplexNumber complexNumber)
        {
            ComplexNumber evaluatedComplex = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber givenComplexNumber = complexNumber;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        givenComplexNumber = givenComplexNumber.Multiply(complexNumber);

                    coefficient = coefficient.Multiply(givenComplexNumber);
                }

                evaluatedComplex = evaluatedComplex.Add(coefficient);
            }

            return evaluatedComplex;
        }

        public override string ToString()
        {
            string polynomeByString = "";
            for (int i = 0; i < Coefficients.Count; i++)
            {
                polynomeByString += Coefficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        polynomeByString += "x";
                    }
                }
                polynomeByString += " + ";
            }
            return polynomeByString.Substring(0, polynomeByString.Length - 3);
        }
    }
}
