using CalculatorLib.BLL.Contracts;

namespace CalculatorLib.BLL.Functions
{
    public class Equal : BaseFunction
    {
        public Equal() : base("=", 1)
        {
        }

        public override bool CanAccept(Command command)
        {
            return command.Type == CommandType.Literal;
        }

        public override ICommandAcceptor Accept(Command command)
        {
            if (command.Type != CommandType.Literal) return this;
            var newCommandAcceptor = Factory.CreateLiteral(null);
            newCommandAcceptor.Accept(command);
            return newCommandAcceptor;
        }
    }
}