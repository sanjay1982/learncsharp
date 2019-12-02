using Calculator.BLL.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.BLL
{
    public class CommandAcceptorFactory : ICommandAcceptorFactory
    {
        private static readonly Dictionary<string, Func<ICalculatorFunction>> Functions = typeof(ExpressionBuilder).Assembly
            .GetTypes().Where(x => x.GetInterfaces().Contains(typeof(ICommandAcceptor)) && x.GetInterfaces().Contains(typeof(ICalculatorFunction)) && !x.IsAbstract)
            .Select(y => Expression.Lambda<Func<ICalculatorFunction>>(
                Expression.New(y.GetConstructor(Type.EmptyTypes) ?? throw new InvalidOperationException())
            ).Compile()).ToDictionary(z => z.Invoke().Name, z => z);

        private static readonly List<Func<ICommandAcceptor>> Literals = typeof(ExpressionBuilder).Assembly
            .GetTypes().Where(x => x.GetInterfaces().Contains(typeof(ICommandAcceptor)) && !x.GetInterfaces().Contains(typeof(ICalculatorFunction)) && !x.IsAbstract)
            .Select(y => Expression.Lambda<Func<ICommandAcceptor>>(
                Expression.New(y.GetConstructor(Type.EmptyTypes) ?? throw new InvalidOperationException())
            ).Compile()).ToList();

        public ICommandAcceptor Create(Command command, ICommandAcceptor previousAcceptor)
        {
            if (command.Type == CommandType.Function)
            {
                if (Functions.TryGetValue(command.Value, out var factory))
                {
                    return Create(factory, previousAcceptor);
                }
            }
            else
            {
                return Create(Literals.First(), previousAcceptor);
            }
            return null;
        }

        private ICommandAcceptor Create<T>(Func<T> factory, ICommandAcceptor previousAcceptor = null) where T : class
        {
            var commandAcceptor = factory?.Invoke() as ICommandAcceptor;
            commandAcceptor.Initialize(this, previousAcceptor);
            return commandAcceptor;
        }

        public ICommandAcceptor CreateLiteral()
        {
            return Create(Literals.First());

        }
    }
}
