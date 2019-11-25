using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Functions
{
    public class SignChange : BaseFunction
    {
        public SignChange() : base("+/-", 1)
        {
        }

        public override decimal Calculate(decimal[] arguments)
        {
            return arguments.FirstOrDefault() * -1;
        }
    }
}
