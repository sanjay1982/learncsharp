using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Calculator.Functions;

namespace Calculator.ViewModels
{
    public interface ICommandAcceptor
    {
        bool Accept(Command command);
    }
    public class DecimalLiteral : ICommandAcceptor
    {
        public DecimalLiteral()
        {
            _literal = "0";
        }
        public DecimalLiteral(object value) : this()
        {
            var test = value.ToString();
            if (Validate(test))
            {
                _literal = test;
            }
        }
        public object Value => _literal.Any(x => x == '.') ? double.Parse(_literal) : long.Parse(_literal);
        public bool IsValid { get; private set; }

        private string _literal;
        public bool Accept(Command command)
        {
            if (command.Type != CommandType.Literal) return false;
            string test = _literal + command.Value;
            if (test.Length > 1 && test.First() == '0')
            {
                test = test.Substring(1);
            }
            if (!Validate(test))
            {
                return false;
            }
            _literal = test;
            return true;
        }
        private bool Validate(string literal)
        {
            return decimal.TryParse(literal,
                      NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                      CultureInfo.InvariantCulture, out var result);
        }
    }
    public class ExpressionBuilder : ICommandAcceptor
    {
        private static readonly Dictionary<string, ICalculatorFunction> Functions = typeof(ExpressionBuilder).Assembly
            .GetTypes().Where(x => x.GetInterfaces().Contains(typeof(ICalculatorFunction)) && !x.IsAbstract)
            .Select(y => Expression.Lambda<Func<ICalculatorFunction>>(
                Expression.New(y.GetConstructor(Type.EmptyTypes))
            ).Compile().Invoke()).ToDictionary(z => z.Name, z => z);

        public ICalculatorFunction Function { get; set; }
        public List<DecimalLiteral> Literals { get; set; } = new List<DecimalLiteral> { new DecimalLiteral() };
        private DecimalLiteral CurrentLiteral => Literals.Last();
        public string ExpressionString { get; private set; }
        public string Value => CurrentLiteral.Value.ToString();
        public bool Accept(Command command)
        {
            if (command.Type == CommandType.Function)
            {
                if (!Functions.TryGetValue(command.Value, out var function)) return false;
                RunFunction();
                Function = function;
                RunFunction(true);
            }
            else
            {
                if (!CurrentLiteral.Accept(command))
                {
                    return false;
                }
            }

            ExpressionString += command.Text;
            return true;
        }

        private void RunFunction(bool addLiteral = false)
        {
            if (Function != null)
            {
                if (Literals.Count >= Function.ArgumentCount)
                {
                    Literals = new List<DecimalLiteral> { new DecimalLiteral(Function.Calculate(Literals.Select(x => x.Value).ToArray())) };
                    Function = null;
                }
                else if(addLiteral)
                {
                    Literals.Add(new DecimalLiteral());
                }
            }
        }
    }
}