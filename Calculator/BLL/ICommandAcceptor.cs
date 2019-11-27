namespace Calculator.BLL
{
    public interface ICommandAcceptor
    {
        bool Accept(Command command);
    }
}