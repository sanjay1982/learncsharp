namespace Calculator.BLL.Contracts
{
    public interface ICommandAcceptorFactory
    {
        ICommandAcceptor Create(Command command, ICommandAcceptor current);

        ICommandAcceptor CreateLiteral(object value);
    }
}