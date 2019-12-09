namespace CalculatorLib.BLL.Contracts
{
    public interface ICommandAcceptor
    {
        object Value { get; }
        string ExpressionString { get; }

        bool CanAccept(Command command);

        ICommandAcceptor Accept(Command command);

        ICommandAcceptor Initialize(ICommandAcceptorFactory commandAcceptorFactory, ICommandAcceptor previousAcceptor);
    }
}