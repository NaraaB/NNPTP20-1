using Microsoft.VisualStudio.TestTools.UnitTesting;
using INPTPZ1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPTPZ1Tests
{
    [TestClass()]
    public class ComplexNumberTest
    {
        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber a = new ComplexNumber()
            {
                RealPart = 10,
                ImaginaryPart = 20
            };
            ComplexNumber b = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = 2
            };

            ComplexNumber actualAnswer = a.Add(b);
            ComplexNumber expectedAnswer = new ComplexNumber()
            {
                RealPart = 11,
                ImaginaryPart = 22
            };

            Assert.AreEqual(expectedAnswer, actualAnswer);
        }
    }
}


