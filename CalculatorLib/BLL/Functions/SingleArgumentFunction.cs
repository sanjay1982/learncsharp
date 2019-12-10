using CalculatorLib.BLL.Contracts;

namespace CalculatorLib.BLL.Functions
{
    public abstract class SingleArgumentFunction : BaseFunction
    {
        protected SingleArgumentFunction(string name, int argumentCount) : base(name, argumentCount)
        {
        }

        protected override ICommandAcceptor OnInitialize(ICommandAcceptor previousAcceptor)
        {
            if (ArgumentCount > 1) return base.OnInitialize(previousAcceptor);
            Literals.Add(previousAcceptor);
            CompletedLiterals = 1;
            return Factory.CreateLiteral(Calculate());
        }
    }
}