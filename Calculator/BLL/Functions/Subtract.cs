﻿namespace Calculator.BLL.Functions
{
    public class Subtract : BaseFunction
    {
        public Subtract() : base("-", 2)
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
}