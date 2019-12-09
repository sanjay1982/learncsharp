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
            Literals.Add(previousAcceptor);
            CompletedLiterals = 1;
            return Factory.CreateLiteral(Calculate());
        }
    }
}