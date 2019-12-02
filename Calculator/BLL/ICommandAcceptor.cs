namespace Calculator.BLL
{
    public interface ICommandAcceptor
    {
        bool CanAccept(Command command);
        ICommandAcceptor Accept(Command command);
        object Value { get; }
        string ExpressionString { get; }

        void Initialize(ICommandAcceptorFactory commandAcceptorFactory, ICommandAcceptor previousAcceptor);
    }
}