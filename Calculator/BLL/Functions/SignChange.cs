using System.Linq;

namespace Calculator.BLL.Functions
{
    public class SignChange : SingleArgumentFunction
    {
        public SignChange() : base("+/-", 1)
        {
        }

        protected override object Calculate(double[] arguments)
        {
            return arguments.First() * -1;
        }

        protected override object Calculate(long[] arguments)
        {
            return arguments.First() * -1;
        }
        
    }
}