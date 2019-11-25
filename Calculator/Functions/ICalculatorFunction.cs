using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Functions
{
    public interface ICalculatorFunction
    {
        string Name { get; }
        int ArgumentCount { get; }

        object Calculate(object[] arguments);
    }
}
