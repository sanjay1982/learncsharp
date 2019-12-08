using System.Globalization;
using System.Linq;
using Calculator.BLL.Contracts;

namespace Calculator.BLL.Literals
{
    public class DecimalLiteral : BaseCommandAcceptor
    {
        private string _literal;

        public DecimalLiteral()
        {
            _literal = "0";
        }

        public DecimalLiteral(object value) : this()
        {
            var test = value?.ToString() ?? "0";
            if (Validate(test)) _literal = test;
        }

        public override object Value => _literal.Any(x => x == '.') ? double.Parse(_literal) : long.Parse(_literal);

        public override bool CanAccept(Command command)
        {
            if (command.Type != CommandType.Literal) return true;
            if (_literal == "0" && command.Value == "0") return false;
            if (_literal.Contains(".") && command.Value == ".") return false;
            if (_literal.Length >= 25) return false;
            return Validate(_literal + command.Value);
        }

        public override ICommandAcceptor Accept(Command command)
        {
            if (command.Type != CommandType.Literal) return Factory.Create(command, this);
            if (!CanAccept(command)) return this;
            _literal += command.Value;
            if (_literal.StartsWith(".")) _literal = "0" + command.Value;

            return this;
        }

        private static bool Validate(string literal)
        {
            return decimal.TryParse(literal,
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture, out _);
        }
    }
}