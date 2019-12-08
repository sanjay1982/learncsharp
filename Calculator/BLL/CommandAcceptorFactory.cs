using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.BLL.Contracts;

namespace Calculator.BLL
{
    public class CommandAcceptorFactory : ICommandAcceptorFactory
    {
        private static readonly Dictionary<string, Func<ICalculatorFunction>> Functions = typeof(CommandAcceptorFactory)
            .Assembly
            .GetTypes().Where(x =>
                x.GetInterfaces().Contains(typeof(ICommandAcceptor)) &&
                x.GetInterfaces().Contains(typeof(ICalculatorFunction)) && !x.IsAbstract)
            .Select(y =>
            {
                ICalculatorFunction Create()
                {
                    y.GetConstructor()
                    return y.GetInstance() as ICalculatorFunction;
                }

                return (Func<ICalculatorFunction>)Create;
            }).ToDictionary(z => z.Invoke().Name, z => z);

        private static readonly List<Func<object, ICommandAcceptor>> Literals = typeof(CommandAcceptorFactory).Assembly
            .GetTypes().Where(x =>
                x.GetInterfaces().Contains(typeof(ICommandAcceptor)) &&
                !x.GetInterfaces().Contains(typeof(ICalculatorFunction)) && !x.IsAbstract)
            .Select(y =>
            {
                ICommandAcceptor Create(object param)
                {
                    return y.GetInstance(param) as ICommandAcceptor;
                }

                return (Func<object, ICommandAcceptor>)Create;
            }).ToList();

        public ICommandAcceptor Create(Command command, ICommandAcceptor previousAcceptor)
        {
            if (command.Type == CommandType.Function)
            {
                if (Functions.TryGetValue(command.Value, out var factory))
                    return Initialize(factory.Invoke() as ICommandAcceptor, previousAcceptor);
            }
            else
            {
                return Initialize(Literals.First().Invoke(null), previousAcceptor);
            }

            return null;
        }

        public ICommandAcceptor CreateLiteral(object value)
        {
            return Initialize(Literals.First().Invoke(value));
        }

        private ICommandAcceptor Initialize(ICommandAcceptor commandAcceptor, ICommandAcceptor previousAcceptor = null)
        {
            return commandAcceptor.Initialize(this, previousAcceptor);
        }
    }
}