namespace CalculatorLib.BLL.Functions
{
    public class Add : BaseFunction
    {
        public Add() : base("+", 2)
        {
        }

        protected override object Calculate(double[] arguments)
        {
            return arguments[0] + arguments[1];
        }

        protected override object Calculate(long[] arguments)
        {
            return arguments[0] + arguments[1];
        }
    }
}