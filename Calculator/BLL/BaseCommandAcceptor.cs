using Calculator.BLL.Contracts;

namespace Calculator.BLL
{
    public abstract class BaseCommandAcceptor : ICommandAcceptor
    {
        protected ICommandAcceptorFactory Factory { get; private set; }
        public abstract object Value { get; }

        public virtual string ExpressionString => Value.ToString();

        public abstract ICommandAcceptor Accept(Command command);

        public ICommandAcceptor Initialize(ICommandAcceptorFactory commandAcceptorFactory,
            ICommandAcceptor previousAcceptor)
        {
            Factory = commandAcceptorFactory;
            return OnInitialize(previousAcceptor);
        }

        public abstract bool CanAccept(Command command);

        protected virtual ICommandAcceptor OnInitialize(ICommandAcceptor previousAcceptor)
        {
            return this;
        }
    }
}