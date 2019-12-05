namespace Calculator.BLL.Functions
{
    public class Divide : BaseFunction
    {
        public Divide() : base("/", 2)
        {
        }

        protected override object Calculate(double[] arguments)
        {
            return arguments[0] / arguments[1];
        }

        protected override object Calculate(long[] arguments)
        {
            return arguments[0] / (double)arguments[1];
        }
    }
}