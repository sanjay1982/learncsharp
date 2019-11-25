using System.Collections.Generic;
using System.Linq;

namespace Calculator.ViewModels
{
    public class LiteralBuilder
    {
        public double Value { get; set; }

        public IEnumerable<string> AcceptedUnaryOperators { get; } = new List<string>
        {
            "+/-"
        };

        public bool CanAccept(Command command)
        {
            return command.Type == CommandType.Literal ||
                   command.Type == CommandType.UnaryOperator && AcceptedUnaryOperators.Any(x => x == command.Value);
        }

        public bool Accept(Command command)
        {
            if (!CanAccept(command)) return false;

            if (command.Type == CommandType.Literal && long.TryParse(command.Value, out var intValue))
                Value = Value * 10 + intValue;
            else
                Value *= -1;

            return true;
        }
    }
}