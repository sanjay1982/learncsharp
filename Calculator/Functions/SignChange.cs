using System.Linq;

namespace Calculator.Functions
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

    public class Add : BaseFunction
    {
        public Add() : base("+", 2)
        {
        }

        public override object Calculate(double[] arguments)
        {
            return arguments[0] + arguments[1];
        }

        public override object Calculate(long[] arguments)
        {
            return arguments[0] + arguments[1];
        }
    }

    public class Subtract : BaseFunction
    {
        public Subtract() : base("+", 2)
        {
        }

        public override object Calculate(double[] arguments)
        {
            return arguments[0] - arguments[1];
        }

        public override object Calculate(long[] arguments)
        {
            return arguments[0] - arguments[1];
        }
    }

    public class Multiply : BaseFunction
    {
        public Multiply() : base("+", 2)
        {
        }

        public override object Calculate(double[] arguments)
        {
            return arguments[0] * arguments[1];
        }

        public override object Calculate(long[] arguments)
        {
            return arguments[0] * arguments[1];
        }
    }

    public class Divide : BaseFunction
    {
        public Divide() : base("+", 2)
        {
        }

        public override object Calculate(double[] arguments)
        {
            return arguments[0] / arguments[1];
        }

        public override object Calculate(long[] arguments)
        {
            return arguments[0] / (double)arguments[1];
        }
    }

    public class Equal : BaseFunction
    {
        public Equal() : base("=", 1)
        {
        }

        public override object Calculate(object[] arguments)
        {
            return arguments.First();
        }
    }
}