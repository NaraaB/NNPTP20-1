using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq.Expressions;
using System.Threading;

namespace INPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private static int canvasSide, iteration, idOfRoot;
        private static double xmin, ymin, xmax, ymax, xstep, ystep;
        private static Bitmap bitmapCanvas;
        private static Color[] colors;
        private static Polynome polynome, derivationOfPolynome;
        private static List<ComplexNumber> roots;
        private static ComplexNumber chosenComplex;

        private static void Initialize()
        {
            bitmapCanvas = new Bitmap(canvasSide, canvasSide);
            xstep = (xmax - xmin) / canvasSide;
            ystep = (ymax - ymin) / canvasSide;

            colors = new Color[] { Color.Red, Color.Green, Color.Blue };
            roots = new List<ComplexNumber>();
            polynome = new Polynome(new List<ComplexNumber>
            {
                new ComplexNumber { RealPart = 1},
                ComplexNumber.Zero,
                ComplexNumber.Zero,
                new ComplexNumber { RealPart = 1},
            });
            derivationOfPolynome = polynome.Derivate();
            Console.WriteLine("f(x) = " + polynome);
            Console.WriteLine("f'(x) = " + derivationOfPolynome);
        }

        static void Main(string[] args)
        {
            ManageInput(args);            
            if (canvasSide > 0)
            {
                Initialize();
                for (int xCoordinate = 0; xCoordinate < canvasSide; xCoordinate++)
                {
                    for (int yCoordinate = 0; yCoordinate < canvasSide; yCoordinate++)
                    {
                        chosenComplex = new ComplexNumber()
                        {
                            RealPart = xmin + yCoordinate * xstep,
                            ImaginaryPart = ymin + xCoordinate * ystep
                        };
                        if (chosenComplex.IsComplexZero())
                        {
                            chosenComplex.RealPart = 0.0001;
                            chosenComplex.ImaginaryPart = 0.0001f;
                        }
                        FindSolutionByNewton();

                        FindIdOfRoot();

                        ColorizePixel(idOfRoot, iteration, yCoordinate, xCoordinate);
                    }
                }
                bitmapCanvas.Save("../../../INPTPZ1_output.png");
                Console.WriteLine("Output image is saved!");
            }
            else
            {
                Console.WriteLine("Invalid Input! Canvas side should be integer greater than 0.");
                return;
            }
            Console.ReadKey();
            return;
        }        

        private static void ManageInput(string[] arguments)
        {
            double[] doubleargs = new double[4];
            if (arguments.Length == 5)
            {
                if (!Int32.TryParse(arguments[0], out canvasSide))
                {
                    Console.WriteLine("Error occured while parsing args[0] to canvasSide");
                    return;
                }
                for (int i = 0; i < doubleargs.Length; i++)
                {
                    if (!Double.TryParse(arguments[i + 1], out doubleargs[i]))
                    {
                        Console.WriteLine("Error occured while parsing args[" + i + 1 + "] to doubleargs[" + i + "]");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Please insert value for square shaped canvas side");
                Console.WriteLine("Length of canvas side =");
                if (!Int32.TryParse(Console.ReadLine(), out canvasSide))
                {
                    Console.WriteLine("Error occured while parsing input to canvasSide value");
                    return;
                }
                Console.WriteLine("Please insert the Minimum and the Maximum values of x and y");
                for (int i = 0; i < doubleargs.Length; i++)
                {
                    string str = (i == 0 || i == 1) ? "x" : "y";
                    str += (i == 0 || i == 2) ? "min= " : "max= ";
                    Console.WriteLine(str);
                    if (!Double.TryParse(Console.ReadLine(), out doubleargs[i]))
                    {
                        Console.WriteLine("Error occured while parsing input to doubleargs[" + i + "]");
                        return;
                    }
                }
            }
            xmin = doubleargs[0]; xmax = doubleargs[1];
            ymin = doubleargs[2]; ymax = doubleargs[3];
        }

        private static void FindSolutionByNewton()
        {
            iteration = 0;
            for (int i = 0; i < 30; i++)
            {
                var diff = polynome.Evaluate(chosenComplex).Divide(derivationOfPolynome.Evaluate(chosenComplex));
                chosenComplex = chosenComplex.Subtract(diff);
                if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)                
                    i--;                
                iteration++;
            }
        }

        private static void FindIdOfRoot()
        {
            idOfRoot = 0;
            bool isRootIdKnown = false;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(chosenComplex.RealPart - roots[i].RealPart, 2) + Math.Pow(chosenComplex.ImaginaryPart - roots[i].ImaginaryPart, 2) <= 0.01)
                {
                    isRootIdKnown = true;
                    idOfRoot = i;
                }
            }
            if (!isRootIdKnown)
            {
                roots.Add(chosenComplex);
                idOfRoot = roots.Count;
            }
        }

        private static void ColorizePixel(int currentIdOfRoot, int currentIteration, int x, int y)
        {
            Color pxlColor = colors[currentIdOfRoot % colors.Length];
            int red = Math.Min(Math.Max(0, pxlColor.R - currentIteration * 2), 255);
            int green = Math.Min(Math.Max(0, pxlColor.G - currentIteration * 2), 255);
            int blue = Math.Min(Math.Max(0, pxlColor.B - currentIteration * 2), 255);
            pxlColor = Color.FromArgb(red, green, blue);
            bitmapCanvas.SetPixel(x, y, pxlColor);
        }
    }
}
