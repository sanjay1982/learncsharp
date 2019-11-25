using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Functions
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
            return Calculate(arguments.Select(Convert.ToDecimal).ToArray());
        }

        public virtual decimal Calculate(decimal[] arguments)
        {
            return 0;
        }
    }
}
