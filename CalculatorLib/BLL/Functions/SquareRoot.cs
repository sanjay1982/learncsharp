using System;
using System.Linq;

namespace CalculatorLib.BLL.Functions
{
    public class SquareRoot : SingleArgumentFunction
    {
        public SquareRoot() : base("sqrt", 1)
        {
        }

        protected override object Calculate(double[] arguments)
        {
            return Math.Sqrt(arguments.First());
        }

        protected override object Calculate(long[] arguments)
        {
            return Math.Sqrt(arguments.First());
        }
    }
}