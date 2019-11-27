using System.Globalization;
using System.Linq;

namespace Calculator.BLL
{
    public class DecimalLiteral : ICommandAcceptor
    {
        private string _literal;

        public DecimalLiteral()
        {
            _literal = "0";
        }

        public DecimalLiteral(object value) : this()
        {
            var test = value.ToString();
            if (Validate(test)) _literal = test;
        }

        public object Value => _literal.Any(x => x == '.') ? double.Parse(_literal) : long.Parse(_literal);

        public bool Accept(Command command)
        {
            if (command.Type != CommandType.Literal) return false;
            var test = _literal + command.Value;
            if (test.Length > 1 && test.First() == '0' && test != "0.")
            {
                test = test.Substring(1);
            }

            if (test.StartsWith("."))
            {
                test = "0" + test;
            }

            if (!Validate(test))
            {
                return false;
            }
            _literal = test;
            return true;
        }

        private static bool Validate(string literal)
        {
            return decimal.TryParse(literal,
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture, out var result);
        }
    }
}