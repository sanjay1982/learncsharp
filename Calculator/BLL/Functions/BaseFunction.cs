using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.BLL.Functions
{
    using Contracts;

    public abstract class BaseFunction : BaseCommandAcceptor, ICalculatorFunction
    {
        protected readonly List<ICommandAcceptor> _literals = new List<ICommandAcceptor>();
        protected int _completedLiterals;
        protected object _value;

        protected BaseFunction(string name, int argumentCount)
        {
            Name = name;
            ArgumentCount = argumentCount;
        }

        public override string ExpressionString => string.Join(Name, _literals.Select(x => x.ExpressionString));
        public override object Value => _value ?? _literals.LastOrDefault()?.Value ?? "0";
        public string Name { get; }
        public int ArgumentCount { get; }
        protected object Calculate() => Calculate(_literals.Select(x => x.Value).ToArray());
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
            if (ArgumentCount > _literals.Count && command.Type != CommandType.Literal) return command.ArgumentCount == 1;
            return true;
        }

        public override ICommandAcceptor Accept(Command command)
        {
            if (!CanAccept(command)) return this;
            if (command.Type != CommandType.Literal)
            {
                if (command.ArgumentCount == 1)
                {
                   _literals[_literals.Count-1]=Factory.Create(command, _literals.LastOrDefault());
                    return this;
                }
                _value = Calculate();
                return Factory.Create(command, this);
            }

            if (_completedLiterals == _literals.Count) _literals.Add(Factory.Create(command, this));

            _literals.Last().Accept(command);

            return this;
        }

        protected override ICommandAcceptor OnInitialize(ICommandAcceptor previousAcceptor)
        {
            if (ArgumentCount == 0) return Factory.CreateLiteral(0);
            _literals.Add(previousAcceptor);
            _completedLiterals = 1;
            return this;
        }
    }
    public abstract class SingleArgumentFunction : BaseFunction
    {
        protected SingleArgumentFunction(string name, int argumentCount) : base(name, argumentCount)
        {
        }

        protected override ICommandAcceptor OnInitialize(ICommandAcceptor previousAcceptor)
        {
            _literals.Add(previousAcceptor);
            _completedLiterals = 1;
            return Factory.CreateLiteral(Calculate());
        }
    }
}