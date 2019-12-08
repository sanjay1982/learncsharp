using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.BLL.Contracts;

namespace Calculator.BLL.Functions
{
    public abstract class BaseFunction : BaseCommandAcceptor, ICalculatorFunction
    {
        protected readonly List<ICommandAcceptor> Literals = new List<ICommandAcceptor>();
        private object _value;
        protected int CompletedLiterals;

        protected BaseFunction(string name, int argumentCount)
        {
            Name = name;
            ArgumentCount = argumentCount;
        }

        public override string ExpressionString => string.Join(Name, Literals.Select(x => x.ExpressionString));
        public override object Value => _value ?? Literals.LastOrDefault()?.Value ?? "0";
        public int ArgumentCount { get; }

        public string Name { get; }

        protected object Calculate()
        {
            return Calculate(Literals.Select(x => x.Value).ToArray());
        }

        protected virtual object Calculate(object[] arguments)
        {
            if (arguments.Length != ArgumentCount) return null;
            return arguments.Any(x => x is float || x is decimal || x is double)
                ? Calculate(arguments.Select(Convert.ToDouble).ToArray())
                : Calculate(arguments.Select(Convert.ToInt64).ToArray());
        }

        protected virtual object Calculate(double[] arguments)
        {
            return 0.0;
        }

        protected virtual object Calculate(long[] arguments)
        {
            return 0;
        }

        public override bool CanAccept(Command command)
        {
            if (command == null) return false;
            if (ArgumentCount > Literals.Count && command.Type != CommandType.Literal)
                return command.ArgumentCount <= 1;
            return true;
        }

        public override ICommandAcceptor Accept(Command command)
        {
            if (!CanAccept(command)) return this;
            if (command.Type != CommandType.Literal)
            {
                if (command.ArgumentCount == 1)
                {
                    if (CompletedLiterals == Literals.Count && ArgumentCount > Literals.Count)
                        Literals.Add(Factory.CreateLiteral(0));
                    Literals[Literals.Count - 1] = Factory.Create(command, Literals.LastOrDefault());
                    return this;
                }

                _value = Calculate();
                return Factory.Create(command, this);
            }

            if (CompletedLiterals == Literals.Count) Literals.Add(Factory.Create(command, this));

            Literals.Last().Accept(command);

            return this;
        }

        protected override ICommandAcceptor OnInitialize(ICommandAcceptor previousAcceptor)
        {
            if (ArgumentCount == 0) return Factory.CreateLiteral(0);
            Literals.Add(previousAcceptor);
            CompletedLiterals = 1;
            return this;
        }
    }
}