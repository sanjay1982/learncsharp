using CalculatorLib.BLL.Contracts;
using CalculatorLib.BLL.Functions;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CalculatorLib.BLL
{
    public class CommandAcceptorFactory : ICommandAcceptorFactory
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly Dictionary<string, Func<ICalculatorFunction>> Functions = typeof(CommandAcceptorFactory)
            .Assembly
            .GetTypes().Where(x =>
                x.GetInterfaces().Contains(typeof(ICommandAcceptor)) &&
                x.GetInterfaces().Contains(typeof(ICalculatorFunction)) && !x.IsAbstract &&
                x.GetConstructor(Type.EmptyTypes) != null)
            .Select(y =>
            {
                ICalculatorFunction Create()
                {
                    return y.GetInstance() as ICalculatorFunction;
                }

                return (Func<ICalculatorFunction>)Create;
            }).ToDictionary(z => z.Invoke().Name, z => z);

        private static readonly List<Func<object, ICommandAcceptor>> Literals = typeof(CommandAcceptorFactory).Assembly
            .GetTypes().Where(x =>
                x.GetInterfaces().Contains(typeof(ICommandAcceptor)) &&
                !x.GetInterfaces().Contains(typeof(ICalculatorFunction)) && !x.IsAbstract &&
                x.GetConstructor(new[] { typeof(object) }) != null)
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
            if (command.Type != CommandType.Function)
            {
                Logger.Debug($"Creating Literal {command.Value}");
                return Initialize(Literals.First().Invoke(null), previousAcceptor);
            }

            if (!Functions.TryGetValue(command.Value, out var factory))
                return CreateFromTypeName(command, previousAcceptor);

            Logger.Debug($"Creating function from factory {command.Value}.");
            return Initialize(factory.Invoke() as ICommandAcceptor, previousAcceptor);
        }

        public ICommandAcceptor CreateLiteral(object value)
        {
            return Initialize(Literals.First().Invoke(value));
        }

        private ICommandAcceptor CreateFromTypeName(Command command, ICommandAcceptor previousAcceptor)
        {
            var index = command.Value.LastIndexOf(".", StringComparison.Ordinal);
            var typeName = command.Value.Substring(0, index);
            var methodName = command.Value.Substring(index + 1);
            var type = Type.GetType(typeName);
            if (type == null)
            {
                Logger.Error($"Failed to create function {command.Value}.");
                return null;
            }

            //Debug.WriteLine(string.Join(Environment.NewLine,
            //    type.GetMethods().Where(x => x.IsStatic).Select(x =>
            //            $"<Command Text=\"{x.Name}\" Value=\"{type.FullName}.{x.Name}\"" +
            //            $" Type=\"Function\" Key=\"None\" ArgumentCount=\"{x.GetParameters().Length}\" />")
            //        .Distinct()));
            var methodInfos = type.GetMethods().Where(x => x.Name == methodName).ToArray();
            if (methodInfos.Length == 0)
            {
                Logger.Error($"Failed to create function for type {command.Value}");
                return null;
            }

            Logger.Debug($"Created function for type {command.Value}");
            return new TypeMethod(type, methodInfos).Initialize(this, previousAcceptor);
        }

        private ICommandAcceptor Initialize(ICommandAcceptor commandAcceptor, ICommandAcceptor previousAcceptor = null)
        {
            return commandAcceptor.Initialize(this, previousAcceptor);
        }
    }
}