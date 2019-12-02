using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Calculator.BLL.Functions;

namespace Calculator.BLL
{
    public class ExpressionBuilder
    {
        //private static readonly Dictionary<string, ICalculatorFunction> Functions = typeof(ExpressionBuilder).Assembly
        //    .GetTypes().Where(x => x.GetInterfaces().Contains(typeof(ICalculatorFunction)) && !x.IsAbstract)
        //    .Select(y => Expression.Lambda<Func<ICalculatorFunction>>(
        //        Expression.New(y.GetConstructor(Type.EmptyTypes) ?? throw new InvalidOperationException())
        //    ).Compile().Invoke()).ToDictionary(z => z.Name, z => z);

        //private DecimalLiteral _currentLiteral;
        //private bool _reset;

        //public ICalculatorFunction Function { get; set; }
        //public List<DecimalLiteral> Literals { get; set; } = new List<DecimalLiteral>();
        //public string ExpressionString { get; private set; }
        //public string Value => _currentLiteral?.Value.ToString() ?? Literals.LastOrDefault()?.Value.ToString() ?? "0";
        //private int LiteralCount => Literals.Count + (_currentLiteral == null ? 0 : 1);

        //public bool Accept(Command command)
        //{
        //    if (command.Type == CommandType.Function)
        //    {
        //        if (!Functions.TryGetValue(command.Value, out var function)) return false;
        //        if (RunCurrentFunction() &&
        //            RunNewFunction(command, function))
        //            return true;
        //    }
        //    else
        //    {
        //        if (_reset)
        //        {
        //            Literals.Clear();
        //            _currentLiteral = null;
        //            _reset = false;
        //        }

        //        _currentLiteral = _currentLiteral ?? new DecimalLiteral();
        //        if (!_currentLiteral.Accept(command)) return false;
        //        ExpressionString += command.Text;
        //    }

        //    return true;
        //}

        //private bool RunCurrentFunction()
        //{
        //    if (Function == null) return true;
        //    if (LiteralCount < Function.ArgumentCount) return false;
        //    CalculateFunction(Function);
        //    ExpressionString = $"({ExpressionString})";
        //    return true;
        //}

        //private void CalculateFunction(ICalculatorFunction function)
        //{
        //    if (_currentLiteral != null)
        //    {
        //        Literals.Add(_currentLiteral);
        //        _currentLiteral = null;
        //    }

        //    var value = new DecimalLiteral(function.Calculate(Literals.Select(x => x.Value).ToArray()));
        //    Literals.Clear();
        //    Literals.Add(value);
        //}

        //private bool RunNewFunction(Command command, ICalculatorFunction function)
        //{
        //    if (function == null) return false;
        //    Function = null;
        //    if (LiteralCount >= function.ArgumentCount)
        //    {
        //        CalculateFunction(function);
        //        ExpressionString = Value;
        //        // _reset = true;
        //    }
        //    else
        //    {
        //        Function = function;
        //        if (_currentLiteral != null) Literals.Add(_currentLiteral);
        //        ExpressionString += command.Text;
        //        _currentLiteral = null;
        //    }

        //    return true;
        //}
    }
}