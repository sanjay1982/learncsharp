using System.Linq;

namespace Calculator.BLL.Functions
{
    public class SignChange : BaseFunction
    {
        public SignChange() : base("+/-", 1)
        {
        }

        public override object Calculate(double[] arguments)
        {
            return arguments.First() * -1;
        }

        public override object Calculate(long[] arguments)
        {
            return arguments.First() * -1;
        }
    }
}