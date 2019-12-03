namespace Calculator.BLL.Contracts
{
    public interface ICommandAcceptor
    {
        object Value { get; }
        string ExpressionString { get; }

        bool CanAccept(Command command);

        ICommandAcceptor Accept(Command command);

        void Initialize(ICommandAcceptorFactory commandAcceptorFactory, ICommandAcceptor previousAcceptor);
    }
}