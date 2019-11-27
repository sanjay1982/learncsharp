using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Calculator.BLL.Functions;

namespace Calculator.BLL
{
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
                if (!CurrentLiteral.Accept(command)) return false;
            }

            ExpressionString += command.Text;
            return true;
        }

        private void RunFunction(bool addLiteral = false)
        {
            if (Function == null) return;
            if (Literals.Count >= Function.ArgumentCount)
            {
                var value = new DecimalLiteral(Function.Calculate(Literals.Select(x => x.Value).ToArray()));
                //ExpressionString += "="+value.Value.ToString();
                Literals = new List<DecimalLiteral>
                    {value};
                Function = null;
            }
            else if (addLiteral)
            {
                Literals.Add(new DecimalLiteral());
            }
        }
    }
}