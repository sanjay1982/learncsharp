using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.BLL.Functions
{
    public abstract class BaseFunction : BaseCommandAcceptor, ICalculatorFunction
    {
        protected BaseFunction(string name, int argumentCount)
        {
            Name = name;
            ArgumentCount = argumentCount;
        }
        private readonly List<ICommandAcceptor> _literals = new List<ICommandAcceptor>();
        private int _completedLiterals = 0;
        private object _value;
        public string Name { get; }
        public int ArgumentCount { get; }

        public override string ExpressionString => string.Join(Name, _literals.Select(x => x.ExpressionString));
        public virtual object Calculate(object[] arguments)
        {
            if (arguments.Length != ArgumentCount) return null;
            return arguments.Any(x => x is float || x is decimal || x is double)
                ? Calculate(arguments.Select(Convert.ToDouble).ToArray())
                : Calculate(arguments.Select(Convert.ToInt64).ToArray());
        }

        public virtual object Calculate(double[] arguments)
        {
            return 0.0;
        }

        public virtual object Calculate(long[] arguments)
        {
            return 0;
        }
        public override object Value => _value ?? _literals.LastOrDefault()?.Value??"0";
        public override bool CanAccept(Command command)
        {
            if (ArgumentCount > _literals.Count && command.Type != CommandType.Literal) return false;
            return true;

        }
        public override ICommandAcceptor Accept(Command command)
        {
            if (!CanAccept(command)) return this;
            if(command.Type != CommandType.Literal)
            {
                if(_literals.Count < ArgumentCount)
                {
                    _literals.Add(Factory.Create(command, this));
                    _completedLiterals++;
                }
                _value = Calculate(_literals.Select(x => x.Value).ToArray());
                return Factory.Create(command, this);
            }
            if(_completedLiterals == _literals.Count)
            {
                _literals.Add(Factory.Create(command, this));
            }
           
            _literals.Last().Accept(command);
            
            return this;

        }
        protected override void OnInitialize(ICommandAcceptor previousAcceptor)
        {
            if (ArgumentCount == 0) return;
            _literals.Add(previousAcceptor);
            _completedLiterals = 1;
        }
    }
}