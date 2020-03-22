using CalculatorLib.BLL.Contracts;
using log4net;
using System.Reflection;

namespace CalculatorLib.BLL
{
    public abstract class BaseCommandAcceptor : ICommandAcceptor
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected ICommandAcceptorFactory Factory { get; private set; }
        public abstract object Value { get; }

        public virtual string ExpressionString => Value.ToString();

        public abstract ICommandAcceptor Accept(Command command);

        public ICommandAcceptor Initialize(ICommandAcceptorFactory commandAcceptorFactory,
            ICommandAcceptor previousAcceptor)
        {
            Logger.Debug($"Initialized command acceptor {GetType().Name}");
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