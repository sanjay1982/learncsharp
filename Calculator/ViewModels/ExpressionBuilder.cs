using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Calculator.Functions;

namespace Calculator.ViewModels
{
    public class ExpressionBuilder
    {
        private static readonly Dictionary<string, ICalculatorFunction> Functions = typeof(ExpressionBuilder).Assembly
            .GetTypes().Where(x => x.GetInterfaces().Contains(typeof(ICalculatorFunction)) && !x.IsAbstract)
            .Select(y => Expression.Lambda<Func<ICalculatorFunction>>(
                Expression.New(y.GetConstructor(Type.EmptyTypes))
            ).Compile().Invoke()).ToDictionary(z => z.Name, z => z);

        private string _lastLiteral = "";
        public string ExpressionString { get; set; }
        public string Value { get; set; }

        public ICalculatorFunction Function { get; set; }
        public List<object> Literals { get; set; }

        private bool CheckOrAccept(Command command, bool accept)
        {
            var status = false;
            if (command.Type == CommandType.Function)
            {
                status =
                    Functions.TryGetValue(command.Value, out var function) && !string.IsNullOrEmpty(_lastLiteral);

                if (accept && status)
                {
                    Literals.Add(_lastLiteral.Any(x => x == '.')
                        ? Convert.ToDouble(_lastLiteral)
                        : Convert.ToInt64(_lastLiteral));
                    if (function.ArgumentCount == Literals.Count)
                    {
                        _lastLiteral = Value = Convert.ToString(function.Calculate(Literals.ToArray()));
                        Literals.Clear();
                    }
                }
            }
            else
            {
                status = string.IsNullOrEmpty(_lastLiteral) ||
                         command.Value == "0" && _lastLiteral.Any(x => x != '0') ||
                         command.Value == "." && _lastLiteral.All(x => x != '.');
                if (accept && status)
                    _lastLiteral = string.Concat(_lastLiteral,
                        string.IsNullOrEmpty(_lastLiteral) && command.Value == "." ? "0" : "", command.Value);
            }

            return status;
        }

        public bool Accept(Command command)
        {
            if (command.Type == CommandType.Function)
            {
                if (!Functions.TryGetValue(command.Value, out var function) || Literals.Count != 1) return false;
                return Literals.Count == 1;
            }

            return true;
        }
    }
}