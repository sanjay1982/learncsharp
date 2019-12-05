namespace Calculator.BLL
{
    using Contracts;

    public abstract class BaseCommandAcceptor : ICommandAcceptor
    {
        protected ICommandAcceptorFactory Factory { get; private set; }
        protected virtual CommandType CommandType { get; }
        public abstract object Value { get; }

        public virtual string ExpressionString => Value.ToString();

        public abstract ICommandAcceptor Accept(Command command);

        public ICommandAcceptor Initialize(ICommandAcceptorFactory commandAcceptorFactory, ICommandAcceptor previousAcceptor)
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