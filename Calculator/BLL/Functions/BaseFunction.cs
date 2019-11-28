﻿using System;
using System.Linq;

namespace Calculator.BLL.Functions
{
    public abstract class BaseFunction : ICalculatorFunction
    {
        protected BaseFunction(string name, int argumentCount)
        {
            Name = name;
            ArgumentCount = argumentCount;
        }

        public string Name { get; }
        public int ArgumentCount { get; }

        public virtual object Calculate(object[] arguments)
        {
            if (arguments.Length != ArgumentCount) return null;
            return arguments.Any(x => x is float || x is decimal || x is double)
                ? Calculate(arguments.Select(Convert.ToDouble).ToArray())
                : Calculate(arguments.Select(Convert.ToInt64).ToArray());
        }

        public virtual object Calculate(double[] arguments)
        {
            return 0.0;
        }

        public virtual object Calculate(long[] arguments)
        {
            return 0;
        }
    }
}