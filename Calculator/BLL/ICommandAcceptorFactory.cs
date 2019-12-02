namespace Calculator.BLL
{
    public interface ICommandAcceptorFactory
    {
        ICommandAcceptor Create(Command command, ICommandAcceptor current);
        ICommandAcceptor CreateLiteral();
    }
}