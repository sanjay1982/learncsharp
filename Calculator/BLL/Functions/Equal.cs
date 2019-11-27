using System.Linq;

namespace Calculator.BLL.Functions
{
    public class Equal : BaseFunction
    {
        public Equal() : base("=", 0)
        {
        }

        public override object Calculate(object[] arguments)
        {
            return arguments.First();
        }
    }
}